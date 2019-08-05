using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class StartTransformPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line65()
		{
			// tag::811a0ff3a0e65bbb869c5654a47892cd[]
			var response0 = new SearchResponse<object>();
			// end::811a0ff3a0e65bbb869c5654a47892cd[]

			response0.MatchesExample(@"POST _data_frame/transforms/ecommerce_transform/_start");
		}
	}
}