using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class StopTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line95()
		{
			// tag::a54a3affc99756ba9cc8b4860fd5206e[]
			var response0 = new SearchResponse<object>();
			// end::a54a3affc99756ba9cc8b4860fd5206e[]

			response0.MatchesExample(@"POST _data_frame/transforms/ecommerce_transform/_stop");
		}
	}
}