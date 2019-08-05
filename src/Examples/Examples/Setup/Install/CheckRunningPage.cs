using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Setup.Install
{
	public class CheckRunningPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line7()
		{
			// tag::3d1ff6097e2359f927c88c2ccdb36252[]
			var response0 = new SearchResponse<object>();
			// end::3d1ff6097e2359f927c88c2ccdb36252[]

			response0.MatchesExample(@"GET /");
		}
	}
}