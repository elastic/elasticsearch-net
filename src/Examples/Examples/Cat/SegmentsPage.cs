using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class SegmentsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line9()
		{
			// tag::6f507269ad5b31d2bb0885c1b18aac1a[]
			var response0 = new SearchResponse<object>();
			// end::6f507269ad5b31d2bb0885c1b18aac1a[]

			response0.MatchesExample(@"GET /_cat/segments?v");
		}
	}
}