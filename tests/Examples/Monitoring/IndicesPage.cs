// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Monitoring
{
	public class IndicesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("monitoring/indices.asciidoc:12")]
		public void Line12()
		{
			// tag::83dfd0852101eca3ba8174c9c38b4e73[]
			var response0 = new SearchResponse<object>();
			// end::83dfd0852101eca3ba8174c9c38b4e73[]

			response0.MatchesExample(@"GET /_template/.monitoring-*");
		}

		[U(Skip = "Example not implemented")]
		[Description("monitoring/indices.asciidoc:29")]
		public void Line29()
		{
			// tag::a63906c63a8681c72d53ee0fcf2ffd35[]
			var response0 = new SearchResponse<object>();
			// end::a63906c63a8681c72d53ee0fcf2ffd35[]

			response0.MatchesExample(@"PUT /_template/custom_monitoring
			{
			    ""index_patterns"": "".monitoring-*"",
			    ""order"": 1,
			    ""settings"": {
			        ""number_of_shards"": 5,
			        ""number_of_replicas"": 2
			    }
			}");
		}
	}
}
