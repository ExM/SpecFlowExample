using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecFlowExample.features.steps_definitions
{
    [Binding]
    public class SiteSearchSteps
    {
        private IWebDriver driver;

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

        [Given(@"User open global web site")]
        public void GivenUserOpenGlobalWebSite()
        {

            driver.Url = "http://www.abbyy.com";
        }

        [Given(@"fill Search field with query ""(.*)""")]
        public void GivenFillSearchFieldWithQuery(string query)
        {
            driver.FindElement(By.Name("searchText")).SendKeys(query);
        }

        [When(@"User press Search button")]
        public void WhenUserPressSearchButton()
        {
            driver.FindElement(By.ClassName("header-search-button")).Click();
        }

        [Then(@"the search results should be on the screen")]
        public void ThenTheSearchResultsShouldBeOnTheScreen()
        {
            driver.FindElement(By.XPath("//h1[text()='Search results']"));
        }
    }
}