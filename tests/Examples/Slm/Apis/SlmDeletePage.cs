using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Slm.Apis
{
	public class SlmDeletePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line66()
		{
			// tag::1a1f3421717ff744ed83232729289bb0[]
			var response0 = new SearchResponse<object>();
			// end::1a1f3421717ff744ed83232729289bb0[]

			response0.MatchesExample(@"DELETE /_slm/policy/daily-snapshots");
		}
	}
}