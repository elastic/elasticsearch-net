using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class StatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line347()
		{
			// tag::861f5f61409dc87f3671293b87839ff7[]
			var response0 = new SearchResponse<object>();
			// end::861f5f61409dc87f3671293b87839ff7[]

			response0.MatchesExample(@"GET /_cluster/stats?human&pretty");
		}

		[U(Skip = "Example not implemented")]
		public void Line591()
		{
			// tag::71c629c44bf3c542a0daacbfc253c4b0[]
			var response0 = new SearchResponse<object>();
			// end::71c629c44bf3c542a0daacbfc253c4b0[]

			response0.MatchesExample(@"GET /_cluster/stats/nodes/node1,node*,master:false");
		}
	}
}