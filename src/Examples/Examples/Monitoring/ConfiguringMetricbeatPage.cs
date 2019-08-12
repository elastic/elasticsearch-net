using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Monitoring
{
	public class ConfiguringMetricbeatPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line32()
		{
			// tag::fb2b8d642e16132eebcff4f8b6d592d1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fb2b8d642e16132eebcff4f8b6d592d1[]

			response0.MatchesExample(@"GET _cluster/settings");

			response1.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""xpack.monitoring.collection.enabled"": true
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line198()
		{
			// tag::519603821dc5b883fc2cf50e3d164084[]
			var response0 = new SearchResponse<object>();
			// end::519603821dc5b883fc2cf50e3d164084[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""xpack.monitoring.elasticsearch.collection.enabled"": false
			  }
			}");
		}
	}
}