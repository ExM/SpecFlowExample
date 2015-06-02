using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace SpecFlowExample.Support
{
	public abstract class SeleniumStepsBase
	{
		protected IWebDriver Selenium
		{
			get { return (IWebDriver) FeatureContext.Current["Browser"]; }
		}
	}
}