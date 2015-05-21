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
        public MainPage mainPage = new MainPage();
        public SearchResultsPage searchResultsPage = new SearchResultsPage();

        [Given(@"User open ABBYY web site")]
        public void GivenUserOpenGlobalWebSite()
        {
            mainPage = MainPage.NavigateTo(driver);
        }

        [Given(@"fill Search field with query ""(.*)""")]
        public void GivenFillSearchFieldWithQuery(string query)
        {                        
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
            searchResultsPage.SearchResultsHeader(driver, searchResultsPage);
        }
    }
}