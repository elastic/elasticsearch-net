using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class AllocationExplainPage : ExampleBase
	{

		[U(Skip = "Example not implemented")]
		public void Line74()
		{
			// tag::2663038cfc46b106edaef607d553c99c[]
			var response0 = new SearchResponse<object>();
			// end::2663038cfc46b106edaef607d553c99c[]

			response0.MatchesExample(@"GET /_cluster/allocation/explain
			{
			  ""index"": ""myindex"",
			  ""shard"": 0,
			  ""primary"": true
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line87()
		{
			// tag::75fb2de2b47c564833ab14049c295384[]
			var response0 = new SearchResponse<object>();
			// end::75fb2de2b47c564833ab14049c295384[]

			response0.MatchesExample(@"GET /_cluster/allocation/explain
			{
			  ""index"": ""myindex"",
			  ""shard"": 0,
			  ""primary"": false,
			  ""current_node"": ""nodeA""                         \<1>
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line104()
		{
			// tag::a21f2014288a2d2c82585be4eb85708c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a21f2014288a2d2c82585be4eb85708c[]

			response0.MatchesExample(@"PUT /idx?master_timeout=1s&timeout=1s
			{""settings"": {""index.routing.allocation.include._name"": ""non_existent_node""} }");

			response1.MatchesExample(@"GET /_cluster/allocation/explain
			{
			  ""index"": ""idx"",
			  ""shard"": 0,
			  ""primary"": true
			}");
		}
	}
}