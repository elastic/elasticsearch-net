// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cluster
{
	public class NodesInfoPage : ExampleBase
	{

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-info.asciidoc:165")]
		public void Line165()
		{
			// tag::3c4d7ef8422d2db423a8f23effcddaa1[]
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
			// end::3c4d7ef8422d2db423a8f23effcddaa1[]

			response0.MatchesExample(@"# return just process");

			response1.MatchesExample(@"GET /_nodes/process");

			response2.MatchesExample(@"# same as above");

			response3.MatchesExample(@"GET /_nodes/_all/process");

			response4.MatchesExample(@"# return just jvm and process of only nodeId1 and nodeId2");

			response5.MatchesExample(@"GET /_nodes/nodeId1,nodeId2/jvm,process");

			response6.MatchesExample(@"# same as above");

			response7.MatchesExample(@"GET /_nodes/nodeId1,nodeId2/info/jvm,process");

			response8.MatchesExample(@"# return all the information of only nodeId1 and nodeId2");

			response9.MatchesExample(@"GET /_nodes/nodeId1,nodeId2/_all");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-info.asciidoc:192")]
		public void Line192()
		{
			// tag::68b64313bf89ec3f2c645da61999dbb4[]
			var response0 = new SearchResponse<object>();
			// end::68b64313bf89ec3f2c645da61999dbb4[]

			response0.MatchesExample(@"GET /_nodes/plugins");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-info.asciidoc:263")]
		public void Line263()
		{
			// tag::0c464965126cc09e6812716a145991d4[]
			var response0 = new SearchResponse<object>();
			// end::0c464965126cc09e6812716a145991d4[]

			response0.MatchesExample(@"GET /_nodes/ingest");
		}
	}
}