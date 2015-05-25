using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SpecFlowExample.Features.Pages
{
    class PageBase
    {
        private IWebDriver Driver { get; set; }

        public PageBase(IWebDriver driver,String titleOfPage)
        {
            Driver = driver;               
            if (driver.Title != titleOfPage)
                throw new NoSuchWindowException(String.Format("PageObjectBase: The Page Title doesnt match. Expected \"{0}\". Got \"{1}\"", titleOfPage, Driver.Title));            
        }
    }
}
