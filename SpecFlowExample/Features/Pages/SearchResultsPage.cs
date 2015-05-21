using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowExample.Features.Pages
{
    public class SearchResultsPage
    {
        [FindsBy(How = How.XPath, Using = "//h1[text()='Search results']")]
        private IWebElement resultsHeader;

        public static SearchResultsPage NavigateTo(IWebDriver driver)
        {
            //driver.Navigate().GoToUrl("http://www.abbyy.com");
            var searchResultsPage = new SearchResultsPage();
            PageFactory.InitElements(driver, searchResultsPage);
            return searchResultsPage;
        }

        public void SearchResultsHeader(IWebDriver driver, SearchResultsPage searchResultsPage)
        {
            PageFactory.InitElements(driver, searchResultsPage);
            StringAssert.AreEqualIgnoringCase(resultsHeader.Text, "Search results");            
        }
    }
}
