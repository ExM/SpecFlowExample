using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowExample.Features.Pages
{
	internal class SearchResultsPage : PageBase
	{
		public SearchResultsPage(IWebDriver driver)
			: base(driver, "Search")
		{
			PageFactory.InitElements(Driver, this);
		}
	}
}
