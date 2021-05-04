// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Root
{
	public class ClusterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster.asciidoc:56")]
		public void Line56()
		{
			// tag::2c602b4ee8f22cda2cdf19bad31da0af[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();

			var response6 = new SearchResponse<object>();

			var response7 = new SearchResponse<object>();

			var response8 = new SearchResponse<object>();

			var response9 = new SearchResponse<object>();

			var response10 = new SearchResponse<object>();

			var response11 = new SearchResponse<object>();

			var response12 = new SearchResponse<object>();

			var response13 = new SearchResponse<object>();

			var response14 = new SearchResponse<object>();
			// end::2c602b4ee8f22cda2cdf19bad31da0af[]

			response0.MatchesExample(@"GET /_nodes
			# Explicitly select all nodes");

			response1.MatchesExample(@"GET /_nodes/_all
			# Select just the local node");

			response2.MatchesExample(@"GET /_nodes/_local
			# Select the elected master node");

			response3.MatchesExample(@"GET /_nodes/_master
			# Select nodes by name, which can include wildcards");

			response4.MatchesExample(@"GET /_nodes/node_name_goes_here");

			response5.MatchesExample(@"GET /_nodes/node_name_goes_*
			# Select nodes by address, which can include wildcards");

			response6.MatchesExample(@"GET /_nodes/10.0.0.3,10.0.0.4");

			response7.MatchesExample(@"GET /_nodes/10.0.0.*
			# Select nodes by role");

			response8.MatchesExample(@"GET /_nodes/_all,master:false");

			response9.MatchesExample(@"GET /_nodes/data:true,ingest:true");

			response10.MatchesExample(@"GET /_nodes/coordinating_only:true");

			response11.MatchesExample(@"GET /_nodes/master:true,voting_only:false
			# Select nodes by custom attribute (e.g. with something like `node.attr.rack: 2` in the configuration file)");

			response12.MatchesExample(@"GET /_nodes/rack:2");

			response13.MatchesExample(@"GET /_nodes/ra*:2");

			response14.MatchesExample(@"GET /_nodes/ra*:2*");
		}
	}
}
