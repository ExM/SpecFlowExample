using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
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

        [BeforeTestRun]
        public static void InitScenario()
        {            
        }

        [AfterScenario]
        public void AfterWebTest()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot(WebDriver);
            }
        }

        [AfterTestRun]
        public static void TearDownScenario()
        {
            if (WebDriver != null) WebDriver.Dispose();
        }
    }
}
