using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.Follow
{
	public class GetFollowStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line38()
		{
			// tag::020c95db88ef356093f03be84893ddf9[]
			var response0 = new SearchResponse<object>();
			// end::020c95db88ef356093f03be84893ddf9[]

			response0.MatchesExample(@"GET /<index>/_ccr/stats");
		}

		[U(Skip = "Example not implemented")]
		public void Line209()
		{
			// tag::8e43bb5b7946143e69d397bb81d87df0[]
			var response0 = new SearchResponse<object>();
			// end::8e43bb5b7946143e69d397bb81d87df0[]

			response0.MatchesExample(@"GET /follower_index/_ccr/stats");
		}
	}
}