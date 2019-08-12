using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class IndicesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line85()
		{
			// tag::073539a7e38be3cdf13008330b6a536a[]
			var response0 = new SearchResponse<object>();
			// end::073539a7e38be3cdf13008330b6a536a[]

			response0.MatchesExample(@"GET /_cat/indices/twi*?v&s=index");
		}
	}
}