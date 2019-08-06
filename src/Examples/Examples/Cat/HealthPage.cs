using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class HealthPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::f8cc4b331a19ff4df8e4a490f906ee69[]
			var response0 = new SearchResponse<object>();
			// end::f8cc4b331a19ff4df8e4a490f906ee69[]

			response0.MatchesExample(@"GET /_cat/health?v");
		}

		[U(Skip = "Example not implemented")]
		public void Line25()
		{
			// tag::ccd9e2cf7181de67cf9ab0df1a02c575[]
			var response0 = new SearchResponse<object>();
			// end::ccd9e2cf7181de67cf9ab0df1a02c575[]

			response0.MatchesExample(@"GET /_cat/health?v&ts=false");
		}
	}
}