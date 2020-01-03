using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class DeleteTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::c0632f2983704d482200d4900e722534[]
			var response0 = new SearchResponse<object>();
			// end::c0632f2983704d482200d4900e722534[]

			response0.MatchesExample(@"DELETE _data_frame/transforms/ecommerce_transform");
		}
	}
}