using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class SearchShardsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line17()
		{
			// tag::49b137a1c0016face219bac3faf41996[]
			var response0 = new SearchResponse<object>();
			// end::49b137a1c0016face219bac3faf41996[]

			response0.MatchesExample(@"GET /twitter/_search_shards");
		}

		[U(Skip = "Example not implemented")]
		public void Line102()
		{
			// tag::a44b7da0091ac75e5571475a4e99bb16[]
			var response0 = new SearchResponse<object>();
			// end::a44b7da0091ac75e5571475a4e99bb16[]

			response0.MatchesExample(@"GET /twitter/_search_shards?routing=foo,bar");
		}
	}
}