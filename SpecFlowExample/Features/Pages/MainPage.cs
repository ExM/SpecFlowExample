using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecFlowExample.Features.Pages
{
    class MainPage : PageBase
    {
        private IWebDriver Driver { get; set; }

        [FindsBy(How = How.XPath, Using = "//title")]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.Name, Using = "searchText")]
        private IWebElement searchField;

        [FindsBy(How = How.ClassName, Using = "header-search-button")]
        private IWebElement searchButton;

        public MainPage(IWebDriver driver)
            : base(driver, "OCR, PDF, ICR, OMR Software - Optical Character Recognition, PDF, and Linguistic Solutions - ABBYY")
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void SearchField(string query)
        {
            searchField.SendKeys(query);
        }

        public void SearchSubmit()
        {
            searchButton.Click();            
        }        
    }
}
