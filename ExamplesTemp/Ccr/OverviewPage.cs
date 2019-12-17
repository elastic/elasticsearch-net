using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr
{
	public class OverviewPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line194()
		{
			// tag::7c5e41a7c0075d87b8f8348a6efa990c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::7c5e41a7c0075d87b8f8348a6efa990c[]

			response0.MatchesExample(@"POST /follower_index/_ccr/pause_follow");

			response1.MatchesExample(@"POST /follower_index/_close");

			response2.MatchesExample(@"PUT /follower_index/_ccr/follow?wait_for_active_shards=1
			{
			  ""remote_cluster"" : ""remote_cluster"",
			  ""leader_index"" : ""leader_index""
			}");
		}
	}
}