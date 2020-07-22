// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cluster
{
	public class StatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/stats.asciidoc:1100")]
		public void Line1100()
		{
			// tag::861f5f61409dc87f3671293b87839ff7[]
			var response0 = new SearchResponse<object>();
			// end::861f5f61409dc87f3671293b87839ff7[]

			response0.MatchesExample(@"GET /_cluster/stats?human&pretty");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/stats.asciidoc:1346")]
		public void Line1346()
		{
			// tag::71c629c44bf3c542a0daacbfc253c4b0[]
			var response0 = new SearchResponse<object>();
			// end::71c629c44bf3c542a0daacbfc253c4b0[]

			response0.MatchesExample(@"GET /_cluster/stats/nodes/node1,node*,master:false");
		}
	}
}
