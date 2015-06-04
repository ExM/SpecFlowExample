using System;
using System.Configuration;
using System.Diagnostics;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace SpecFlowExample.Support
{
	[Binding]
	public class SeleniumSupport
	{
		private readonly IObjectContainer _objectContainer;
		private IWebDriver _webDriver;
		private static string _browser;

		private static bool ReuseWebSession
		{
			get { return ConfigurationManager.AppSettings["ReuseWebSession"] == "true"; }
		}

		public SeleniumSupport(IObjectContainer objectContainer)
		{
			_objectContainer = objectContainer;
		}

		[BeforeScenario]
		public void BeforeWebScenario()
		{
			Start();
		}

		[AfterStep]
		public void ScreenshotAfterTest()
		{
			if (ScenarioContext.Current.TestError != null)
			{
				_webDriver.TakeScreenshot();
			}
		}

		[AfterScenario]
		public void AfterWebScenario()
		{
			if (!ReuseWebSession)
				Stop();
		}

		public static void Set(string browser)
		{
			_browser = browser;
		}

		private IWebDriver GetDriver(string browser)
		{
			IWebDriver webDriver = null;
			switch (browser)
			{
				case "Chrome":
					webDriver = new ChromeDriver(@"Support\Drivers");
					break;
				case "Firefox":
					webDriver = new FirefoxDriver();
					break;
				case "IE":
					webDriver = new InternetExplorerDriver(@"Support\Drivers",
						new InternetExplorerOptions
						{
							IntroduceInstabilityByIgnoringProtectedModeSettings = true,
							IgnoreZoomLevel = true,
							EnableNativeEvents = false
						});
					break;
				default:
					throw new NotSupportedException(String.Format("Browser '{0}' does not exists.", browser));
			}

			return webDriver;
		}

		public void Start()
		{
			_webDriver = GetDriver(_browser);

			_objectContainer.RegisterInstanceAs(_webDriver);

			_webDriver.Manage().Timeouts().ImplicitlyWait(DefaultTimeout);
			_webDriver.Manage().Window.Maximize();

			Trace("Selenium started");
		}

		public void Stop()
		{
			if (_webDriver == null)
				return;

			try
			{
				_webDriver.Quit();
				_webDriver.Dispose();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex, "Selenium stop error");
			}
			_webDriver = null;
			Trace("Selenium stopped");
		}

		private static void Trace(string message)
		{
			Console.WriteLine("-> {0}", message);
		}

		public static TimeSpan DefaultTimeout
		{
			get { return TimeSpan.FromSeconds(10); }
		}
	}
}