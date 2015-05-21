using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowExample.Features.Pages
{
    public class MainPage
    {
        [FindsBy(How = How.Name, Using = "searchText")]
        private IWebElement searchField;

        [FindsBy(How = How.ClassName, Using = "header-search-button")]
        private IWebElement searchButton;

        public static MainPage NavigateTo(IWebDriver driver)
        {            
            driver.Navigate().GoToUrl("http://www.abbyy.com");
            var mainPage = new MainPage();
            PageFactory.InitElements(driver, mainPage);
            return mainPage;
        }
        public void SearchField(string query)
        {
            searchField.SendKeys(query);
        }

        public void SearchSubmit()
        {
            searchButton.Click();            
        }

        public static IWebDriver driver { get; set; }
    }
}
