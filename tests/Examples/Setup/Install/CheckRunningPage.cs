using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Setup.Install
{
	public class CheckRunningPage : ExampleBase
	{
		[U]
		[Description("setup/install/check-running.asciidoc:7")]
		public void Line7()
		{
			// tag::3d1ff6097e2359f927c88c2ccdb36252[]
			var nodeInfoResponse = client.RootNodeInfo();
			// end::3d1ff6097e2359f927c88c2ccdb36252[]

			nodeInfoResponse.MatchesExample(@"GET /");
		}
	}
}
