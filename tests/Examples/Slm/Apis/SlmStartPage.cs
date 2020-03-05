using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Slm.Apis
{
	public class SlmStartPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-start.asciidoc:42")]
		public void Line42()
		{
			// tag::371962cf63e65c10026177c6a1bad0b6[]
			var response0 = new SearchResponse<object>();
			// end::371962cf63e65c10026177c6a1bad0b6[]

			response0.MatchesExample(@"POST _slm/start");
		}
	}
}