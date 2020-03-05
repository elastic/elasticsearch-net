using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.Follow
{
	public class PostPauseFollowPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/post-pause-follow.asciidoc:30")]
		public void Line30()
		{
			// tag::483d669ec0768bc4e275a568c6164704[]
			var response0 = new SearchResponse<object>();
			// end::483d669ec0768bc4e275a568c6164704[]

			response0.MatchesExample(@"POST /<follower_index>/_ccr/pause_follow");
		}

		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/follow/post-pause-follow.asciidoc:63")]
		public void Line63()
		{
			// tag::d3263afc69b6f969b9bbd8738cd07b97[]
			var response0 = new SearchResponse<object>();
			// end::d3263afc69b6f969b9bbd8738cd07b97[]

			response0.MatchesExample(@"POST /follower_index/_ccr/pause_follow");
		}
	}
}