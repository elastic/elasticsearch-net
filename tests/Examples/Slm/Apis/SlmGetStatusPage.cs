using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Slm.Apis
{
	public class SlmGetStatusPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line43()
		{
			// tag::cde4104a29dfe942d55863cdd8718627[]
			var response0 = new SearchResponse<object>();
			// end::cde4104a29dfe942d55863cdd8718627[]

			response0.MatchesExample(@"GET _slm/status");
		}
	}
}