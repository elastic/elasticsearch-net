using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class StatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line12()
		{
			// tag::78c4035e4fbf6851140660f6ed2a1fa5[]
			var response0 = new SearchResponse<object>();
			// end::78c4035e4fbf6851140660f6ed2a1fa5[]

			response0.MatchesExample(@"GET /_stats");
		}

		[U(Skip = "Example not implemented")]
		public void Line20()
		{
			// tag::e0b2f56c34e33ff52f8f9658be2f7ca1[]
			var response0 = new SearchResponse<object>();
			// end::e0b2f56c34e33ff52f8f9658be2f7ca1[]

			response0.MatchesExample(@"GET /index1,index2/_stats");
		}

		[U(Skip = "Example not implemented")]
		public void Line78()
		{
			// tag::45c55ce8b2df147cd68b8f151a36a8d8[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::45c55ce8b2df147cd68b8f151a36a8d8[]

			response0.MatchesExample(@"# Get back stats for merge and refresh only for all indices");

			response1.MatchesExample(@"GET /_stats/merge,refresh
			# Get back stats for type1 and type2 documents for the my_index index");

			response2.MatchesExample(@"GET /my_index/_stats/indexing?types=type1,type2
			# Get back just search stats for group1 and group2");

			response3.MatchesExample(@"GET /_stats/search?groups=group1,group2");
		}
	}
}