using System;
using System.Configuration;
using System.Diagnostics;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;
using System.Collections.Generic;

namespace SpecFlowExample.Support
{
	[Binding]
	public class SeleniumSupport
	{
		private static Dictionary<string, IWebDriver> _webDriverPool = new Dictionary<string, IWebDriver>();

		private static bool ReuseWebSession
		{
			get { return ConfigurationManager.AppSettings["ReuseWebSession"] == "true"; }
		}

		public SeleniumSupport()
		{
			Trace("new SeleniumSupport");
		}

		[BeforeScenario]
		public void BeforeWebScenario()
		{
			var webDriver = GetWebDriver(FeatureContext.Current.Get<string>("browserName"));
			FeatureContext.Current.Set<IWebDriver>(webDriver);
		}

		[AfterStep]
		public void ScreenshotAfterTest()
		{
			if (ScenarioContext.Current.TestError != null)
				FeatureContext.Current.Get<IWebDriver>().TakeScreenshot();
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
			Trace("AfterTestRun");
			foreach (var webDriver in _webDriverPool.Values)
				webDriver.SafeStop();
		}

		private IWebDriver CreateDriver(string browser)
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

			webDriver.Manage().Timeouts().ImplicitlyWait(DefaultTimeout);
			webDriver.Manage().Window.Maximize();
			return webDriver;
		}

		public IWebDriver GetWebDriver(string browserName)
		{
			if (_webDriverPool.ContainsKey(browserName))
			{
				if(ReuseWebSession)
				{
					Trace("get from cache");
					return _webDriverPool[browserName];
				}
				else
				{
					_webDriverPool[browserName].SafeStop();
					_webDriverPool[browserName] = CreateDriver(browserName);
					return _webDriverPool[browserName];
				}
			}
			else
			{
				var result = CreateDriver(browserName);
				_webDriverPool.Add(browserName, result);
				Trace("Selenium created");
				return result;
			}
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