using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class NodesUsagePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line58()
		{
			// tag::3d6a56dd3d93ece0e3da3fb66b4696d3[]
			var response0 = new SearchResponse<object>();
			// end::3d6a56dd3d93ece0e3da3fb66b4696d3[]

			response0.MatchesExample(@"GET _nodes/usage");
		}
	}
}