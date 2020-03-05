using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ingest.Apis.Enrich
{
	public class EnrichStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/enrich/enrich-stats.asciidoc:14")]
		public void Line14()
		{
			// tag::84e2cf7417c9e0c9e6f3c23031001440[]
			var response0 = new SearchResponse<object>();
			// end::84e2cf7417c9e0c9e6f3c23031001440[]

			response0.MatchesExample(@"GET /_enrich/_stats");
		}
	}
}