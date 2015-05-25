using SpecFlowExample.Features.Pages;
using SpecFlowExample.Support;
using TechTalk.SpecFlow;

namespace SpecFlowExample.features.steps_definitions
{
    [Binding]
    public class SiteSearchSteps : SeleniumStepsBase
    {
        private MainPage mainPage;

        [Given(@"User open ABBYY web site")]
        public void GivenUserOpenGlobalWebSite()
        {
            selenium.NavigateTo("/");
        }

        [Given(@"fill Search field with query ""(.*)""")]
        public void GivenFillSearchFieldWithQuery(string query)
        {
            mainPage = new MainPage(selenium);
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
            SearchResultsPage searchResultsPage = new SearchResultsPage(selenium);
        }
    }
}