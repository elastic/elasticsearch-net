using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class NodesHotThreadsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line9()
		{
			// tag::77c099c97ea6911e2dd6e996da7dcca0[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::77c099c97ea6911e2dd6e996da7dcca0[]

			response0.MatchesExample(@"GET /_nodes/hot_threads");

			response1.MatchesExample(@"GET /_nodes/nodeId1,nodeId2/hot_threads");
		}
	}
}