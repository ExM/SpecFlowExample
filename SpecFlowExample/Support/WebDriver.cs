using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

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
                                _driver = new ChromeDriver();
                                ConfigureDriver();
                                break;
                            case "Firefox":
                                _driver = new FirefoxDriver();
                                ConfigureDriver();
                                break;
                            case "IE":
                                _driver = new InternetExplorerDriver();
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
    }
}
