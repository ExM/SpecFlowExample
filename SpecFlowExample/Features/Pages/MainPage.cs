using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowExample.Features.Pages
{
	internal class MainPage : PageBase
	{
		[FindsBy(How = How.Name, Using = "searchText")]
		private IWebElement _searchField;

		[FindsBy(How = How.ClassName, Using = "header-search-button")]
		private IWebElement _searchButton;

		public MainPage(IWebDriver driver)
			: base(driver, "OCR, PDF, ICR, OMR Software - Optical Character Recognition, PDF, and Linguistic Solutions - ABBYY")
		{
			PageFactory.InitElements(driver, this);
		}

		public void SearchField(string query)
		{
			_searchField.SendKeys(query);
		}

		public void SearchSubmit()
		{
			_searchButton.Click();
		}
	}
}
