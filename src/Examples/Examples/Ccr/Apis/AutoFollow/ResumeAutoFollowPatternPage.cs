using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.AutoFollow
{
	public class ResumeAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line70()
		{
			// tag::f2e854b6c99659ccc1824e86c096e433[]
			var response0 = new SearchResponse<object>();
			// end::f2e854b6c99659ccc1824e86c096e433[]

			response0.MatchesExample(@"POST /_ccr/auto_follow/my_auto_follow_pattern/resume");
		}
	}
}