using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.SearchableSnapshots.Apis
{
	public class GetStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("searchable-snapshots/apis/get-stats.asciidoc:73")]
		public void Line73()
		{
			// tag::806b19ef04f9b5acc99a59da73aff282[]
			var response0 = new SearchResponse<object>();
			// end::806b19ef04f9b5acc99a59da73aff282[]

			response0.MatchesExample(@"GET /docs/_searchable_snapshots/stats");
		}
	}
}