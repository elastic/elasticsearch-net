using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class AllocationExplainPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line19()
		{
			// tag::e4cd381f35dcaa151dd93cf259e50ae6[]
			var response0 = new SearchResponse<object>();
			// end::e4cd381f35dcaa151dd93cf259e50ae6[]

			response0.MatchesExample(@"PUT /myindex");
		}

		[U]
		[SkipExample]
		public void Line28()
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

		[U]
		[SkipExample]
		public void Line48()
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

		[U]
		[SkipExample]
		public void Line65()
		{
			// tag::45803e6cb9fee2b430dcf63d50fb7a2b[]
			var response0 = new SearchResponse<object>();
			// end::45803e6cb9fee2b430dcf63d50fb7a2b[]

			response0.MatchesExample(@"GET /_cluster/allocation/explain");
		}

		[U]
		[SkipExample]
		public void Line145()
		{
			// tag::fb99aaf2e89e70c96c2c79c2ce7a36f1[]
			var response0 = new SearchResponse<object>();
			// end::fb99aaf2e89e70c96c2c79c2ce7a36f1[]

			response0.MatchesExample(@"GET /_cluster/allocation/explain?include_disk_info=true");
		}

		[U]
		[SkipExample]
		public void Line154()
		{
			// tag::681419ddc44c9f7914f88be834ae2b44[]
			var response0 = new SearchResponse<object>();
			// end::681419ddc44c9f7914f88be834ae2b44[]

			response0.MatchesExample(@"GET /_cluster/allocation/explain?include_yes_decisions=true");
		}
	}
}