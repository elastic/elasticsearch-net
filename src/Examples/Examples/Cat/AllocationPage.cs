using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class AllocationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line41()
		{
			// tag::5c7ece1f30267adabdb832424871900a[]
			var response0 = new SearchResponse<object>();
			// end::5c7ece1f30267adabdb832424871900a[]

			response0.MatchesExample(@"GET /_cat/allocation?v");
		}
	}
}