using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace SpecFlowExample.Support
{
    [Binding]
    public static class SeleniumSupport
    {
        private static bool ReuseWebSession
        {
            get { return ConfigurationManager.AppSettings["ReuseWebSession"] == "true"; }
        }

        [BeforeScenario]
        public static void BeforeWebScenario()
        {
            SeleniumController.Instance.Start();
        }

        [AfterStep]
        public static void ScreenshotAfterTest()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                SeleniumController.Instance.TakeScreenshot();
            }
        }

        [AfterScenario]
        public static void AfterWebScenario()
        {
            if (!ReuseWebSession)
                SeleniumController.Instance.Stop();
        }        

        [AfterTestRun]
        public static void AfterWebFeature()
        {
            SeleniumController.Instance.Stop();
        }
    }
}