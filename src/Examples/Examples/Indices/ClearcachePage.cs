using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class ClearcachePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line8()
		{
			// tag::486eee2c8e75520f825fec08c1fbd67e[]
			var response0 = new SearchResponse<object>();
			// end::486eee2c8e75520f825fec08c1fbd67e[]

			response0.MatchesExample(@"POST /twitter/_cache/clear");
		}

		[U(Skip = "Example not implemented")]
		public void Line18()
		{
			// tag::e4a86070ec20da0a7f604e17a12f482e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::e4a86070ec20da0a7f604e17a12f482e[]

			response0.MatchesExample(@"POST /twitter/_cache/clear?query=true      \<1>");

			response1.MatchesExample(@"POST /twitter/_cache/clear?request=true    \<2>");

			response2.MatchesExample(@"POST /twitter/_cache/clear?fielddata=true   \<3>");
		}

		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::62069c4118d79daf9612b29659b16627[]
			var response0 = new SearchResponse<object>();
			// end::62069c4118d79daf9612b29659b16627[]

			response0.MatchesExample(@"POST /twitter/_cache/clear?fields=foo,bar   \<1>");
		}

		[U(Skip = "Example not implemented")]
		public void Line49()
		{
			// tag::389d962b8aa57186c7f94b83aea16c4b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::389d962b8aa57186c7f94b83aea16c4b[]

			response0.MatchesExample(@"POST /kimchy,elasticsearch/_cache/clear");

			response1.MatchesExample(@"POST /_cache/clear");
		}
	}
}