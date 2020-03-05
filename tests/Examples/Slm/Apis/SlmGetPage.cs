using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Slm.Apis
{
	public class SlmGetPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-get.asciidoc:72")]
		public void Line72()
		{
			// tag::b4f9fe8808cb27a210b162e7aaba261d[]
			var response0 = new SearchResponse<object>();
			// end::b4f9fe8808cb27a210b162e7aaba261d[]

			response0.MatchesExample(@"GET /_slm/policy/daily-snapshots?human");
		}

		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-get.asciidoc:123")]
		public void Line123()
		{
			// tag::bc2dd9e5ed37f98016ecf53f968d2211[]
			var response0 = new SearchResponse<object>();
			// end::bc2dd9e5ed37f98016ecf53f968d2211[]

			response0.MatchesExample(@"GET /_slm/policy");
		}
	}
}