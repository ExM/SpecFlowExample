using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SpecFlowExample.Features.Pages
{
    public class MainPage
    {        
        public void SearchField(IWebDriver driver, string query)
        {
            driver.FindElement(By.Name("searchText")).SendKeys(query);
        }

        public void SearchSubmit(IWebDriver driver)
        {
            driver.FindElement(By.ClassName("header-search-button")).Click();
        }
    }
}
