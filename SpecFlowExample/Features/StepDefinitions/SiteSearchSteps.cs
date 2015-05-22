using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using SpecFlowExample.Features.Pages;
using SpecFlowExample.Features.StepDefinitions;
using TechTalk.SpecFlow;

namespace SpecFlowExample.features.steps_definitions
{
    [Binding]
    public class SiteSearchSteps : BaseSteps
    {
        private MainPage mainPage;

        [Given(@"User open ABBYY web site")]
        public void GivenUserOpenGlobalWebSite()
        {
            Visit("http://www.abbyy.com");
        }

        [Given(@"fill Search field with query ""(.*)""")]
        public void GivenFillSearchFieldWithQuery(string query)
        {
            mainPage = new MainPage(WebDriver);
            mainPage.SearchField(query);
        }

        [When(@"User press Search button")]
        public void WhenUserPressSearchButton()
        {
            mainPage.SearchSubmit();
        }

        [Then(@"the search results should be on the screen")]
        public void ThenTheSearchResultsShouldBeOnTheScreen()
        {
            SearchResultsPage searchResultsPage = new SearchResultsPage(WebDriver);
        }
    }
}