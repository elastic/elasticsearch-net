using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class AliasExistsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/alias-exists.asciidoc:12")]
		public void Line12()
		{
			// tag::83e388644f60178c8de0d0e4247ee4c6[]
			var response0 = new SearchResponse<object>();
			// end::83e388644f60178c8de0d0e4247ee4c6[]

			response0.MatchesExample(@"HEAD /_alias/alias1");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/alias-exists.asciidoc:62")]
		public void Line62()
		{
			// tag::666785827827be4b5252ec859c354d30[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::666785827827be4b5252ec859c354d30[]

			response0.MatchesExample(@"HEAD /_alias/2030");

			response1.MatchesExample(@"HEAD /_alias/20*");

			response2.MatchesExample(@"HEAD /logs_20302801/_alias/*");
		}
	}
}