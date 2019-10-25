using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Transform.Apis
{
	public class StartTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line63()
		{
			// tag::01bc0f2ed30eb3dd23511d01ce0ac6e1[]
			var response0 = new SearchResponse<object>();
			// end::01bc0f2ed30eb3dd23511d01ce0ac6e1[]

			response0.MatchesExample(@"POST _transform/ecommerce_transform/_start");
		}
	}
}