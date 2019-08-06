using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class NodesStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line11()
		{
			// tag::46157b875b3af6322b19c9fcf4668b93[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::46157b875b3af6322b19c9fcf4668b93[]

			response0.MatchesExample(@"GET /_nodes/stats");

			response1.MatchesExample(@"GET /_nodes/nodeId1,nodeId2/stats");
		}

		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::5457c94f0039c6b95c7f9f305d0c6b58[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();
			// end::5457c94f0039c6b95c7f9f305d0c6b58[]

			response0.MatchesExample(@"# return just indices");

			response1.MatchesExample(@"GET /_nodes/stats/indices");

			response2.MatchesExample(@"# return just os and process");

			response3.MatchesExample(@"GET /_nodes/stats/os,process");

			response4.MatchesExample(@"# return just process for node with IP address 10.0.0.1");

			response5.MatchesExample(@"GET /_nodes/10.0.0.1/stats/process");
		}

		[U(Skip = "Example not implemented")]
		public void Line313()
		{
			// tag::573e292263dc1e6f08f4363f25018e57[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();

			var response6 = new SearchResponse<object>();

			var response7 = new SearchResponse<object>();
			// end::573e292263dc1e6f08f4363f25018e57[]

			response0.MatchesExample(@"# Fielddata summarised by node");

			response1.MatchesExample(@"GET /_nodes/stats/indices/fielddata?fields=field1,field2");

			response2.MatchesExample(@"# Fielddata summarised by node and index");

			response3.MatchesExample(@"GET /_nodes/stats/indices/fielddata?level=indices&fields=field1,field2");

			response4.MatchesExample(@"# Fielddata summarised by node, index, and shard");

			response5.MatchesExample(@"GET /_nodes/stats/indices/fielddata?level=shards&fields=field1,field2");

			response6.MatchesExample(@"# You can use wildcards for field names");

			response7.MatchesExample(@"GET /_nodes/stats/indices/fielddata?fields=field*");
		}

		[U(Skip = "Example not implemented")]
		public void Line355()
		{
			// tag::bd68666ca2e0be12f7624016317a62bc[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::bd68666ca2e0be12f7624016317a62bc[]

			response0.MatchesExample(@"# All groups with all stats");

			response1.MatchesExample(@"GET /_nodes/stats?groups=_all");

			response2.MatchesExample(@"# Some groups from just the indices stats");

			response3.MatchesExample(@"GET /_nodes/stats/indices?groups=foo,bar");
		}
	}
}