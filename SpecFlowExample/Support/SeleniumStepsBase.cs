using OpenQA.Selenium;

namespace SpecFlowExample.Support
{
	public abstract class SeleniumStepsBase
	{        
		protected IWebDriver Selenium
		{
            get { return SeleniumController.Get().Selenium; }
		}
	}
}