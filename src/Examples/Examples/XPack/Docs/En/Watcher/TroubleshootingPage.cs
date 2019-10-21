using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Watcher
{
	public class TroubleshootingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line18()
		{
			// tag::2856a5ceff1861aa9a78099f1c517fe7[]
			var response0 = new SearchResponse<object>();
			// end::2856a5ceff1861aa9a78099f1c517fe7[]

			response0.MatchesExample(@"GET .watches/_mapping");
		}

		[U(Skip = "Example not implemented")]
		public void Line33()
		{
			// tag::e905543b281e9c41395304da76ed2ea3[]
			var response0 = new SearchResponse<object>();
			// end::e905543b281e9c41395304da76ed2ea3[]

			response0.MatchesExample(@"DELETE .watches");
		}
	}
}