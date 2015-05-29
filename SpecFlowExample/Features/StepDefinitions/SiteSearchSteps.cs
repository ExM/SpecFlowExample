using SpecFlowExample.Features.Pages;
using SpecFlowExample.Support;
using TechTalk.SpecFlow;

namespace SpecFlowExample.Features.StepDefinitions
{
	[Binding]
	public class SiteSearchSteps : SeleniumStepsBase
	{
		private MainPage _mainPage;

		[Given(@"User open ABBYY web site")]
		public void GivenUserOpenGlobalWebSite()
		{
			Selenium.NavigateTo("/");
		}

		[Given(@"fill Search field with query ""(.*)""")]
		public void GivenFillSearchFieldWithQuery(string query)
		{
			_mainPage = new MainPage(Selenium);
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
			SearchResultsPage searchResultsPage = new SearchResultsPage(Selenium);
			searchResultsPage.IsTitleCorrect();
		}
	}
}