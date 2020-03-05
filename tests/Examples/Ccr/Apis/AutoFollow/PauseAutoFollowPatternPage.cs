using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.AutoFollow
{
	public class PauseAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line76()
		{
			// tag::b25256ed615cd837461b0bfa590526b7[]
			var response0 = new SearchResponse<object>();
			// end::b25256ed615cd837461b0bfa590526b7[]

			response0.MatchesExample(@"POST /_ccr/auto_follow/my_auto_follow_pattern/pause");
		}
	}
}