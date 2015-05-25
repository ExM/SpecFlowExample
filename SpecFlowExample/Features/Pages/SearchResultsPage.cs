using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowExample.Features.Pages
{
    class SearchResultsPage : PageBase
    {
        private IWebDriver Driver { get; set; }

        [FindsBy(How = How.XPath, Using = "//title")]
        public IWebElement Title { get; set; }

        public SearchResultsPage(IWebDriver driver)
            : base(driver, "Search")
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
        }
    }
}
