using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
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
