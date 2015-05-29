using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace SpecFlowExample.Features.Pages
{
	class PageBase
	{
		private IWebDriver Driver { get; set; }
		private string Title { get; set; }

		public PageBase(IWebDriver driver, string titleOfPage)
		{
			Driver = driver;
			Title = titleOfPage;
		}

		public void IsTitleCorrect()
		{
			Assert.AreEqual(Title, Driver.Title);
		}
	}
}
