using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis.Enrich
{
	public class ExecuteEnrichPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line40()
		{
			// tag::66c64bffe3a15cf260baa0c0118aa4ea[]
			var response0 = new SearchResponse<object>();
			// end::66c64bffe3a15cf260baa0c0118aa4ea[]

			response0.MatchesExample(@"PUT /_enrich/policy/my-policy/_execute");
		}
	}
}