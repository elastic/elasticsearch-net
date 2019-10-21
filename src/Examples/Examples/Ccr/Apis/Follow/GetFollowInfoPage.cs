using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.Follow
{
	public class GetFollowInfoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line36()
		{
			// tag::b2440b492149b705ef107137fdccb0c2[]
			var response0 = new SearchResponse<object>();
			// end::b2440b492149b705ef107137fdccb0c2[]

			response0.MatchesExample(@"GET /<index>/_ccr/info");
		}

		[U(Skip = "Example not implemented")]
		public void Line138()
		{
			// tag::a520168c1c8b454a8f102d6a13027c73[]
			var response0 = new SearchResponse<object>();
			// end::a520168c1c8b454a8f102d6a13027c73[]

			response0.MatchesExample(@"GET /follower_index/_ccr/info");
		}
	}
}