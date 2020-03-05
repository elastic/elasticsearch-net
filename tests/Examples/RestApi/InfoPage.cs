using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.RestApi
{
	public class InfoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rest-api/info.asciidoc:45")]
		public void Line45()
		{
			// tag::9054187cbab5c9e1c4ca2a4dba6a5db0[]
			var response0 = new SearchResponse<object>();
			// end::9054187cbab5c9e1c4ca2a4dba6a5db0[]

			response0.MatchesExample(@"GET /_xpack");
		}

		[U(Skip = "Example not implemented")]
		[Description("rest-api/info.asciidoc:159")]
		public void Line159()
		{
			// tag::b11a0675e49df0709be693297ca73a2c[]
			var response0 = new SearchResponse<object>();
			// end::b11a0675e49df0709be693297ca73a2c[]

			response0.MatchesExample(@"GET /_xpack?categories=build,features");
		}

		[U(Skip = "Example not implemented")]
		[Description("rest-api/info.asciidoc:166")]
		public void Line166()
		{
			// tag::4ed946065faa92f9950f04e402676a97[]
			var response0 = new SearchResponse<object>();
			// end::4ed946065faa92f9950f04e402676a97[]

			response0.MatchesExample(@"GET /_xpack?human=false");
		}
	}
}