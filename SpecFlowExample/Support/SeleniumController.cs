using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace SpecFlowExample.Support
{
    public class SeleniumController
    {
        public static SeleniumController Instance = new SeleniumController();

        public IWebDriver Selenium { get; private set; }

        private void Trace(string message)
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
                        Selenium = new ChromeDriver(@"..\..\Support\Drivers");
                        break;
                    case "Firefox":
                        Selenium = new FirefoxDriver();
                        break;
                    case "IE":
                        var options = new InternetExplorerOptions();
                        options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                        options.IgnoreZoomLevel = true;
                        options.EnableNativeEvents = false;
                        string path = @"..\..\Support\Drivers";
                        Selenium = new InternetExplorerDriver(path, options);
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
    }
}