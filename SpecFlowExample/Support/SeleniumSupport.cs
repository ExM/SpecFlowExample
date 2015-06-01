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
		    SeleniumController.Get().Start();
		}

		[AfterStep]
		public static void ScreenshotAfterTest()
		{
			if (ScenarioContext.Current.TestError != null)
			{
                SeleniumController.Get().TakeScreenshot();
			}
		}

		[AfterScenario]
		public static void AfterWebScenario()
		{
			if (!ReuseWebSession)
                SeleniumController.Get().Stop();
		}

		[AfterTestRun]
		public static void AfterWebFeature()
		{
            SeleniumController.Get().Stop();
		}
	}
}