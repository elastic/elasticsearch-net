using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Setup.Install
{
	public class CheckRunningPage : ExampleBase
	{
		[U]
		public void Line7()
		{
			// tag::3d1ff6097e2359f927c88c2ccdb36252[]
			var infoResponse = client.RootNodeInfo();
			// end::3d1ff6097e2359f927c88c2ccdb36252[]

			infoResponse.MatchesExample(@"GET /");
		}
	}
}
