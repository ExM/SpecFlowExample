using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SpecFlowExample.Features.Pages
{
    public class SearchResultsPage
    {
        public void SearchResultsHeader(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//h1[text()='Search results']"));
        }
    }
}
