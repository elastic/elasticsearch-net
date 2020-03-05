using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Slm.Apis
{
	public class SlmExecuteRetentionPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-execute-retention.asciidoc:35")]
		public void Line35()
		{
			// tag::e71d300cd87f09a9527cf45395dd7eb1[]
			var response0 = new SearchResponse<object>();
			// end::e71d300cd87f09a9527cf45395dd7eb1[]

			response0.MatchesExample(@"POST /_slm/_execute_retention");
		}
	}
}