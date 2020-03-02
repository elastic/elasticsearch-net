using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class DataframeanalyticsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line124()
		{
			// tag::7c6f205c98da14c68d3d936639462dd3[]
			var response0 = new SearchResponse<object>();
			// end::7c6f205c98da14c68d3d936639462dd3[]

			response0.MatchesExample(@"GET _cat/ml/data_frame/analytics?v");
		}
	}
}