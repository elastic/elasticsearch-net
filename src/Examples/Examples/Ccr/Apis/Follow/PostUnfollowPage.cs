using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.Follow
{
	public class PostUnfollowPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::f6d493650b4344f17297b568016fb445[]
			var response0 = new SearchResponse<object>();
			// end::f6d493650b4344f17297b568016fb445[]

			response0.MatchesExample(@"POST /<follower_index>/_ccr/unfollow");
		}

		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::6a350a17701e8c8158407191f2718b66[]
			var response0 = new SearchResponse<object>();
			// end::6a350a17701e8c8158407191f2718b66[]

			response0.MatchesExample(@"POST /follower_index/_ccr/unfollow");
		}
	}
}