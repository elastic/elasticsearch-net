using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Slm.Apis
{
	public class SlmStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-stats.asciidoc:27")]
		public void Line27()
		{
			// tag::55e8ddf643726dec51531ada0bec7143[]
			var response0 = new SearchResponse<object>();
			// end::55e8ddf643726dec51531ada0bec7143[]

			response0.MatchesExample(@"GET /_slm/stats");
		}
	}
}