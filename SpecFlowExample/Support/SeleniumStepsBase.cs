using OpenQA.Selenium;

namespace SpecFlowExample.Support
{
    public abstract class SeleniumStepsBase
    {
        protected IWebDriver selenium
        {
            get { return SeleniumController.Instance.Selenium; }
        }
    }
}