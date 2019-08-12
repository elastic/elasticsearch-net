using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr.Apis.AutoFollow
{
	public class GetAutoFollowPatternPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line43()
		{
			// tag::5e124875d97c27362ae858160ae1c6d5[]
			var response0 = new SearchResponse<object>();
			// end::5e124875d97c27362ae858160ae1c6d5[]

			response0.MatchesExample(@"GET /_ccr/auto_follow/");
		}

		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::d56a9d89282df56adbbc34b91390ac17[]
			var response0 = new SearchResponse<object>();
			// end::d56a9d89282df56adbbc34b91390ac17[]

			response0.MatchesExample(@"GET /_ccr/auto_follow/<auto_follow_pattern_name>");
		}

		[U(Skip = "Example not implemented")]
		public void Line83()
		{
			// tag::79f33e05b203eb46eef7958fbc95ef77[]
			var response0 = new SearchResponse<object>();
			// end::79f33e05b203eb46eef7958fbc95ef77[]

			response0.MatchesExample(@"GET /_ccr/auto_follow/my_auto_follow_pattern");
		}
	}
}