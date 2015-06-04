using System;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace SpecFlowExample.Support
{
	public static class WebDriverExtensions
	{
		public static string GetTextBoxValue(this IWebDriver browser, string field)
		{
			IWebElement control = GetFieldControl(browser, field);

			return control.GetAttribute("value");
		}

		public static void SetTextBoxValue(this IWebDriver browser, string field, string value)
		{
			var control = GetFieldControl(browser, field);
			var wait = new WebDriverWait(browser, SeleniumSupport.DefaultTimeout);
			if (!String.IsNullOrEmpty(control.GetAttribute("value")))
			{
				control.Clear();
				wait.Until(_ => String.IsNullOrEmpty(control.GetAttribute("value")));
			}

			control.SendKeys(value);
			System.Threading.Thread.Sleep(100);
		}

		public static void TakeScreenshot(this IWebDriver browser)
		{
			try
			{
				var fileNameBase = string.Format("error_{0}_{1}_{2}",
					FeatureContext.Current.FeatureInfo.Title.ToIdentifier(),
					ScenarioContext.Current.ScenarioInfo.Title.ToIdentifier(),
					DateTime.Now.ToString("yyyyMMdd_HHmmss"));

				var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");
				if (!Directory.Exists(artifactDirectory))
					Directory.CreateDirectory(artifactDirectory);

				string pageSource = browser.PageSource;
				string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");
				File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
				Console.WriteLine("Page source: {0}", new Uri(sourceFilePath));

				ITakesScreenshot takesScreenshot = browser as ITakesScreenshot;

				if (takesScreenshot != null)
				{
					var screenshot = takesScreenshot.GetScreenshot();

					string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

					screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);

					Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error while taking screenshot: {0}", ex);
			}
		}

		public static void SubmitForm(this IWebDriver browser, string formId = null)
		{
			var form = formId == null ? GetForm(browser) : browser.FindElements(By.Id(formId)).First();
			form.Submit();
			System.Threading.Thread.Sleep(100);
		}

		public static void ClickButton(this IWebDriver browser, string buttonId)
		{
			browser.FindElements(By.Id(buttonId)).First().Click();
		}

		private static IWebElement GetFieldControl(IWebDriver browser, string field)
		{
			var form = GetForm(browser);
			return form.FindElement(By.Id(field));
		}

		private static IWebElement GetForm(IWebDriver browser)
		{
			return browser.FindElements(By.TagName("form")).First();
		}

		public static void NavigateTo(this IWebDriver browser, string relativeUrl)
		{
			browser.Navigate().GoToUrl(new Uri(new Uri(ConfigurationManager.AppSettings["baseURL"]), relativeUrl));
		}

		public static DropDown GetDropDown(this IWebDriver browser, string id)
		{
			return browser.FindElement(By.Id(id)).AsDropDown();
		}

		public static DropDown AsDropDown(this IWebElement e)
		{
			return new DropDown(e);
		}

		public class DropDown
		{
			private readonly IWebElement _dropDown;

			public DropDown(IWebElement dropDown)
			{
				_dropDown = dropDown;

				if (dropDown.TagName != "select")
					throw new ArgumentException("Invalid web element type");
			}

			public string SelectedValue
			{
				get
				{
					var selectedOption = _dropDown.FindElements(By.TagName("option")).FirstOrDefault(e => e.Selected);
					if (selectedOption == null)
						return null;

					return selectedOption.GetAttribute("value");

				}
				set
				{
					new SelectElement(_dropDown).SelectByValue(value);
				}
			}
		}
	}
}
