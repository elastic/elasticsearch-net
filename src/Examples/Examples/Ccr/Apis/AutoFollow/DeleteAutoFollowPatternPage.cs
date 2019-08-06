using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.AutoFollow
{
	public class DeleteAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::2f2580ea420e1836d922fe48fa8ada97[]
			var response0 = new SearchResponse<object>();
			// end::2f2580ea420e1836d922fe48fa8ada97[]

			response0.MatchesExample(@"DELETE /_ccr/auto_follow/<auto_follow_pattern_name>");
		}

		[U(Skip = "Example not implemented")]
		public void Line68()
		{
			// tag::d4ef6ac034c4d42cb75d830ec69146e6[]
			var response0 = new SearchResponse<object>();
			// end::d4ef6ac034c4d42cb75d830ec69146e6[]

			response0.MatchesExample(@"DELETE /_ccr/auto_follow/my_auto_follow_pattern");
		}
	}
}