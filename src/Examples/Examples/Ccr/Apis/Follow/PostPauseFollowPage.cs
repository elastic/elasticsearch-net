using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.Follow
{
	public class PostPauseFollowPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line31()
		{
			// tag::483d669ec0768bc4e275a568c6164704[]
			var response0 = new SearchResponse<object>();
			// end::483d669ec0768bc4e275a568c6164704[]

			response0.MatchesExample(@"POST /<follower_index>/_ccr/pause_follow");
		}

		[U(Skip = "Example not implemented")]
		public void Line65()
		{
			// tag::d3263afc69b6f969b9bbd8738cd07b97[]
			var response0 = new SearchResponse<object>();
			// end::d3263afc69b6f969b9bbd8738cd07b97[]

			response0.MatchesExample(@"POST /follower_index/_ccr/pause_follow");
		}
	}
}