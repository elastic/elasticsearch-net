using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.Follow
{
	public class PostForgetFollowerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line38()
		{
			// tag::f4fdfe52ecba65eec6beb30d8deb8bbf[]
			var response0 = new SearchResponse<object>();
			// end::f4fdfe52ecba65eec6beb30d8deb8bbf[]

			response0.MatchesExample(@"POST /<leader_index>/_ccr/forget_follower
			{
			  ""follower_cluster"" : ""<follower_cluster>"",
			  ""follower_index"" : ""<follower_index>"",
			  ""follower_index_uuid"" : ""<follower_index_uuid>"",
			  ""leader_remote_cluster"" : ""<leader_remote_cluster>""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line131()
		{
			// tag::07c07f6d497b1a3012aa4320f830e09e[]
			var response0 = new SearchResponse<object>();
			// end::07c07f6d497b1a3012aa4320f830e09e[]

			response0.MatchesExample(@"POST /leader_index/_ccr/forget_follower
			{
			  ""follower_cluster"" : ""follower_cluster"",
			  ""follower_index"" : ""follower_index"",
			  ""follower_index_uuid"" : ""vYpnaWPRQB6mNspmoCeYyA"",
			  ""leader_remote_cluster"" : ""leader_cluster""
			}");
		}
	}
}