using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class NodesUsagePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::488751d6f5baddadd84f6f390d910b07[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::488751d6f5baddadd84f6f390d910b07[]

			response0.MatchesExample(@"GET _nodes/usage");

			response1.MatchesExample(@"GET _nodes/nodeId1,nodeId2/usage");
		}
	}
}