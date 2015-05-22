using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using SpecFlowExample.Support;

namespace SpecFlowExample.Features.StepDefinitions
{
    [Binding]
    public class BaseSteps : SeleniumDriver
    {
        public static IWebDriver driver;        

        [BeforeScenario]
        public void InitScenario()
        {                        
        }

        [AfterScenario]
        public void TearDownScenario()
        {
            if (WebDriver != null) WebDriver.Quit();
        }
    }
}
