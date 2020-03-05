using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ccr.Apis.AutoFollow
{
	public class ResumeAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ccr/apis/auto-follow/resume-auto-follow-pattern.asciidoc:77")]
		public void Line77()
		{
			// tag::f2e854b6c99659ccc1824e86c096e433[]
			var response0 = new SearchResponse<object>();
			// end::f2e854b6c99659ccc1824e86c096e433[]

			response0.MatchesExample(@"POST /_ccr/auto_follow/my_auto_follow_pattern/resume");
		}
	}
}