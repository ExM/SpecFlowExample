using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace SpecFlowExample.StepsDefinitions
{
    public class BaseStepDefinitions
    {
        public IWebDriver driver;
        public string baseURL;

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new FirefoxDriver();
            baseURL = "http://www.abbyy.com/";
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }
    }
}
