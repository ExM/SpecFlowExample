using OpenQA.Selenium;
using SpecFlowExample.Features.Pages;
using SpecFlowExample.Support;
using TechTalk.SpecFlow;

namespace SpecFlowExample.Features.StepDefinitions
{
	[Binding]
	public class SiteSearchSteps
	{
		private readonly MainPage _mainPage;
		private IWebDriver _webDriver;

		public SiteSearchSteps(IWebDriver webDriver)
		{
			_webDriver = webDriver;
			_mainPage = new MainPage(_webDriver);
		}

		[Given(@"User open ABBYY web site")]
		public void GivenUserOpenGlobalWebSite()
		{
			_webDriver.NavigateTo("/");
		}

		[Given(@"fill Search field with query ""(.*)""")]
		public void GivenFillSearchFieldWithQuery(string query)
		{
			_mainPage.SearchField(query);
		}

		[When(@"User press Search button")]
		public void WhenUserPressSearchButton()
		{
			_mainPage.SearchSubmit();
		}

		[Then(@"the search results should be on the screen")]
		public void ThenTheSearchResultsShouldBeOnTheScreen()
		{
			var searchResultsPage = new SearchResultsPage(_webDriver);
			searchResultsPage.IsTitleCorrect();
		}
	}
}