using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Licensing
{
	public class GetTrialStatusPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/get-trial-status.asciidoc:43")]
		public void Line43()
		{
			// tag::88cf60d3310a56d8ae12704abc05b565[]
			var response0 = new SearchResponse<object>();
			// end::88cf60d3310a56d8ae12704abc05b565[]

			response0.MatchesExample(@"GET /_license/trial_status");
		}
	}
}