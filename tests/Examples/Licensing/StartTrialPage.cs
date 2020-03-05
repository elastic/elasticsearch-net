using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Licensing
{
	public class StartTrialPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/start-trial.asciidoc:47")]
		public void Line47()
		{
			// tag::37f1f2e75ed95308ae436bbbb8d5645e[]
			var response0 = new SearchResponse<object>();
			// end::37f1f2e75ed95308ae436bbbb8d5645e[]

			response0.MatchesExample(@"POST /_license/start_trial?acknowledge=true");
		}
	}
}