using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;

namespace SpecFlowExample.features.pages
{
    class MainPage
    {
        public void openPage(string homepage, FirefoxDriver driver)
        {            
            driver.Navigate().GoToUrl(homepage);            
        }

        public void searchFor(string searchTerm, FirefoxDriver driver)
        {
            driver.FindElementByName("searchText").SendKeys(searchTerm);
        }

        public void clickSearch(FirefoxDriver driver)
        {
            driver.FindElementByClassName("icon header-search-button").Click();
        }
    }
}
