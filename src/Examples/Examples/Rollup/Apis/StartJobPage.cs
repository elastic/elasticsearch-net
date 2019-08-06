using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Rollup.Apis
{
	public class StartJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line51()
		{
			// tag::618c9d42284c067891fb57034a4fd834[]
			var response0 = new SearchResponse<object>();
			// end::618c9d42284c067891fb57034a4fd834[]

			response0.MatchesExample(@"POST _rollup/job/sensor/_start");
		}
	}
}