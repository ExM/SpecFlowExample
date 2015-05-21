using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecFlowExample.Features.StepDefinitions
{
    [Binding]
    public class BaseSteps
    {
        public IWebDriver driver;        

        [BeforeScenario]
        public void InitScenario()
        {
            driver = new ChromeDriver();            
        }

        [AfterScenario]
        public void TearDownScenario()
        {
            driver.Dispose();
        }
    }
}
