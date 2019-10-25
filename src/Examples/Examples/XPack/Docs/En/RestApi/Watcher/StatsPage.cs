using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class StatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line84()
		{
			// tag::17266cee5eaaddf08e5534bf580a1910[]
			var response0 = new SearchResponse<object>();
			// end::17266cee5eaaddf08e5534bf580a1910[]

			response0.MatchesExample(@"GET _watcher/stats");
		}

		[U(Skip = "Example not implemented")]
		public void Line112()
		{
			// tag::3ed79871d956bfb2d6d2721d7272520c[]
			var response0 = new SearchResponse<object>();
			// end::3ed79871d956bfb2d6d2721d7272520c[]

			response0.MatchesExample(@"GET _watcher/stats?metric=current_watches");
		}

		[U(Skip = "Example not implemented")]
		public void Line119()
		{
			// tag::56b6b50b174a935d368301ebd717231d[]
			var response0 = new SearchResponse<object>();
			// end::56b6b50b174a935d368301ebd717231d[]

			response0.MatchesExample(@"GET _watcher/stats/current_watches");
		}

		[U(Skip = "Example not implemented")]
		public void Line163()
		{
			// tag::6244204213f60edf2f23295f9059f2c9[]
			var response0 = new SearchResponse<object>();
			// end::6244204213f60edf2f23295f9059f2c9[]

			response0.MatchesExample(@"GET _watcher/stats/queued_watches");
		}
	}
}