using System;
using System.Configuration;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

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
                        Selenium = new InternetExplorerDriver(@"..\..\Support\Drivers");
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