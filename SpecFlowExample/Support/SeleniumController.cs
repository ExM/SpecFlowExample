using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace SpecFlowExample.Support
{
	public class SeleniumController
	{
		public static SeleniumController Instance = new SeleniumController();

		public IWebDriver Selenium { get; private set; }

		private static void Trace(string message)
		{
			Console.WriteLine("-> {0}", message);
		}

		public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

		public void Start()
		{
			if (Selenium != null)
				return;

			string driverConfig = ConfigurationManager.AppSettings["baseURL"];
			if (!String.IsNullOrEmpty(driverConfig))
			{
				switch (ConfigurationManager.AppSettings["browser"])
				{
					case "Chrome":
						Selenium = new ChromeDriver(@"Support\Drivers");
						break;
					case "Firefox":
						Selenium = new FirefoxDriver();
						break;
					case "IE":
						Selenium = new InternetExplorerDriver(@"Support\Drivers",
							new InternetExplorerOptions
							{
								IntroduceInstabilityByIgnoringProtectedModeSettings = true,
								IgnoreZoomLevel = true,
								EnableNativeEvents = false
							});
						break;
					default:
						Console.WriteLine("App.config key error.");
						Console.WriteLine("Defaulting to Firefox");
						Selenium = new FirefoxDriver();
						break;
				}
			}
			else
			{
				Console.WriteLine("* * * * DEFAULTMODE * * * *");
				Console.WriteLine("App.config key not present.");
				Selenium = new FirefoxDriver();
			}
			Selenium.Manage().Timeouts().ImplicitlyWait(DefaultTimeout);
			Selenium.Manage().Window.Maximize();
			Trace("Selenium started");
		}

		public void Stop()
		{
			if (Selenium == null)
				return;

			try
			{
				Selenium.Quit();
				Selenium.Dispose();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex, "Selenium stop error");
			}
			Selenium = null;
			Trace("Selenium stopped");
		}

		public void TakeScreenshot()
		{
			try
			{
				var fileNameBase = string.Format("error_{0}_{1}_{2}",
					FeatureContext.Current.FeatureInfo.Title.ToIdentifier(),
					ScenarioContext.Current.ScenarioInfo.Title.ToIdentifier(),
					DateTime.Now.ToString("yyyyMMdd_HHmmss"));

				var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");
				if (!Directory.Exists(artifactDirectory))
					Directory.CreateDirectory(artifactDirectory);

				string pageSource = Selenium.PageSource;
				string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");
				File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
				Console.WriteLine("Page source: {0}", new Uri(sourceFilePath));

				ITakesScreenshot takesScreenshot = Selenium as ITakesScreenshot;

				if (takesScreenshot != null)
				{
					var screenshot = takesScreenshot.GetScreenshot();

					string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

					screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);

					Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error while taking screenshot: {0}", ex);
			}
		}
	}
}