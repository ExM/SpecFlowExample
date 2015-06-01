using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SpecFlowExample.Support;

namespace SpecFlowExample.Features
{
    [TestFixture(Browsers.InternetExplorer)]
    [TestFixture(Browsers.Firefox)]
    [TestFixture(Browsers.Chrome)]
    public partial class SiteSearchFeature 
    {
        public SiteSearchFeature(string browser)
        {            
            SeleniumController.Get(browser);
        }
    }
}
