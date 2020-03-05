using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class NodesReloadSecureSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line54()
		{
			// tag::72f20e645e118715b6197c2087b4db86[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::72f20e645e118715b6197c2087b4db86[]

			response0.MatchesExample(@"POST _nodes/reload_secure_settings");

			response1.MatchesExample(@"POST _nodes/nodeId1,nodeId2/reload_secure_settings");
		}
	}
}