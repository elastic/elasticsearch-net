using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Rollup.Apis
{
	public class StopJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line76()
		{
			// tag::07a5fdeb7805cec1d28ba288b28f5ff5[]
			var response0 = new SearchResponse<object>();
			// end::07a5fdeb7805cec1d28ba288b28f5ff5[]

			response0.MatchesExample(@"POST _rollup/job/sensor/_stop?wait_for_completion=true&timeout=10s");
		}
	}
}