using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis
{
	public class GetCcrStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/get-ccr-stats.asciidoc:37")]
		public void Line37()
		{
			// tag::9b30a69fec54cf01f7af1b04a6e15239[]
			var response0 = new SearchResponse<object>();
			// end::9b30a69fec54cf01f7af1b04a6e15239[]

			response0.MatchesExample(@"GET /_ccr/stats");
		}
	}
}