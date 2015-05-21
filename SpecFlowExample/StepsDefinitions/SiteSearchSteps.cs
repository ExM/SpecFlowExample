using System;
using NUnit.Framework;
using SpecFlowExample.StepsDefinitions;
using TechTalk.SpecFlow;

namespace SpecFlowExample.features.steps_definitions
{

    [Binding]
    public class SiteSearchSteps : BaseStepDefinitions
    {
        [Given(@"User open global web site")]
        public void GivenUserOpenGlobalWebSite()
        {
            BeforeScenario();
        }

        [Given(@"fill Search field with query ""(.*)""")]
        public void GivenFillSearchFieldWithQuery(string query)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"User press Search button")]
        public void WhenUserPressSearchButton()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the search results should be on the screen")]
        public void ThenTheSearchResultsShouldBeOnTheScreen()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
