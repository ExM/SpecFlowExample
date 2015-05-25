using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace SpecFlowExample.Support
{
    public class SeleniumDriver
    {
        private static IWebDriver _driver;
        protected static IWebDriver WebDriver
        {
            get
            {
                if (_driver == null)
                {
                    string driverConfig = ConfigurationManager.AppSettings["browser"];
                    if (!String.IsNullOrEmpty(driverConfig))
                    {
                        switch (ConfigurationManager.AppSettings["browser"])
                        {
                            case "Chrome":
                                _driver = new ChromeDriver(@"..\..\Support\Drivers");
                                ConfigureDriver();
                                break;
                            case "Firefox":
                                _driver = new FirefoxDriver();
                                ConfigureDriver();
                                break;
                            case "IE":

                                _driver = new InternetExplorerDriver(@"..\..\Support\Drivers");
                                ConfigureDriver();
                                break;
                            default:
                                Console.WriteLine("App.config key error.");
                                Console.WriteLine("Defaulting to Firefox");
                                _driver = new FirefoxDriver();
                                ConfigureDriver();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("* * * * DEFAULTMODE * * * *");
                        Console.WriteLine("App.config key not present.");
                        _driver = new FirefoxDriver();
                        ConfigureDriver();
                    }
                }
                return _driver;
            }
        }
        internal static void ConfigureDriver()
        {
            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 30));
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Manage().Window.Maximize();
        }
        public static void Visit(String url)
        {
            var rootUrl = new Uri(url);
            WebDriver.Navigate().GoToUrl(rootUrl);
        }

        public void TakeScreenshot(IWebDriver driver)
        {
            try
            {
                string fileNameBase = string.Format("error_{0}_{1}_{2}",
                                                    FeatureContext.Current.FeatureInfo.Title.ToIdentifier(),
                                                    ScenarioContext.Current.ScenarioInfo.Title.ToIdentifier(),
                                                    DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");
                if (!Directory.Exists(artifactDirectory))
                    Directory.CreateDirectory(artifactDirectory);

                string pageSource = driver.PageSource;
                string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");
                File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
                Console.WriteLine("Page source: {0}", new Uri(sourceFilePath));

                ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

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
