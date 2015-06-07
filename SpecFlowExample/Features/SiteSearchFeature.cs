using NUnit.Framework;
using SpecFlowExample.Support;
using TechTalk.SpecFlow;

namespace SpecFlowExample.Features
{
	[TestFixture("IE")]
	[TestFixture("Firefox")]
	[TestFixture("Chrome")]
	public partial class SiteSearchFeature
	{
		private string _browserName;

		public SiteSearchFeature(string browserName)
		{
			_browserName = browserName;
		}

		[SetUp]
		public void SetUp()
		{
			FeatureContext.Current.Set(_browserName, "browserName");
		}
	}
}
