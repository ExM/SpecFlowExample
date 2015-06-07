using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowExample.Features.Pages
{
	internal class PageBase
	{
		protected IWebDriver Driver { get; private set; }

		private string ExpectedTitle { get; set; }

		[FindsBy(How = How.XPath, Using = "//title")]
		public IWebElement Title { get; set; }

		public PageBase(IWebDriver driver, string expectedTitle)
		{
			Driver = driver;
			ExpectedTitle = expectedTitle;
		}

		public void IsTitleCorrect()
		{
			Assert.AreEqual(ExpectedTitle, Driver.Title);
		}
	}
}
