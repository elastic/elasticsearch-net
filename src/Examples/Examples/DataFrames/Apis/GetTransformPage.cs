using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class GetTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line99()
		{
			// tag::59b3dc4f4c270e136435c62d30e78982[]
			var response0 = new SearchResponse<object>();
			// end::59b3dc4f4c270e136435c62d30e78982[]

			response0.MatchesExample(@"GET _data_frame/transforms?size=10");
		}

		[U(Skip = "Example not implemented")]
		public void Line109()
		{
			// tag::432f71eed8e670a14195f22c1a557bf7[]
			var response0 = new SearchResponse<object>();
			// end::432f71eed8e670a14195f22c1a557bf7[]

			response0.MatchesExample(@"GET _data_frame/transforms/ecommerce_transform");
		}
	}
}