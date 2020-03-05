using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Modules.Cluster
{
	public class AllocationFilteringPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line22()
		{
			// tag::281ae12918af10b6377ec760eaa844ce[]
			var response0 = new SearchResponse<object>();
			// end::281ae12918af10b6377ec760eaa844ce[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""transient"" : {
			    ""cluster.routing.allocation.exclude._ip"" : ""10.0.0.1""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line65()
		{
			// tag::07474768b8f9d532b524c15e512736f4[]
			var response0 = new SearchResponse<object>();
			// end::07474768b8f9d532b524c15e512736f4[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""transient"": {
			    ""cluster.routing.allocation.exclude._ip"": ""192.168.2.*""
			  }
			}");
		}
	}
}