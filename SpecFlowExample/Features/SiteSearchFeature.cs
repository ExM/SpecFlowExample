using NUnit.Framework;
using SpecFlowExample.Support;

namespace SpecFlowExample.Features
{
	[TestFixture("IE")]
	[TestFixture("Firefox")]
	[TestFixture("Chrome")]
	public partial class SiteSearchFeature
	{
		public SiteSearchFeature(string browser)
		{
			SeleniumController.Set(browser);
		}
	}
}
