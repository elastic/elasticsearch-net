using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class ShardStoresPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line19()
		{
			// tag::897f7ceaa110aa68e1f13ef7791810c5[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();
			// end::897f7ceaa110aa68e1f13ef7791810c5[]

			response0.MatchesExample(@"# return information of only index test");

			response1.MatchesExample(@"GET /test/_shard_stores");

			response2.MatchesExample(@"# return information of only test1 and test2 indices");

			response3.MatchesExample(@"GET /test1,test2/_shard_stores");

			response4.MatchesExample(@"# return information of all indices");

			response5.MatchesExample(@"GET /_shard_stores");
		}

		[U(Skip = "Example not implemented")]
		public void Line39()
		{
			// tag::3545261682af72f4bee57f2bac0a9590[]
			var response0 = new SearchResponse<object>();
			// end::3545261682af72f4bee57f2bac0a9590[]

			response0.MatchesExample(@"GET /_shard_stores?status=green");
		}
	}
}