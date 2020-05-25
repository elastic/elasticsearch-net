// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cluster
{
	public class HealthPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/health.asciidoc:35")]
		public void Line35()
		{
			// tag::04f5dd677c777bcb15d7d5fa63275fc8[]
			var response0 = new SearchResponse<object>();
			// end::04f5dd677c777bcb15d7d5fa63275fc8[]

			response0.MatchesExample(@"GET /_cluster/health?wait_for_status=yellow&timeout=50s");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/health.asciidoc:143")]
		public void Line143()
		{
			// tag::b02e4907c9936c1adc16ccce9d49900d[]
			var response0 = new SearchResponse<object>();
			// end::b02e4907c9936c1adc16ccce9d49900d[]

			response0.MatchesExample(@"GET _cluster/health");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/health.asciidoc:179")]
		public void Line179()
		{
			// tag::c48264ec5d9b9679fddd72e5c44425b9[]
			var response0 = new SearchResponse<object>();
			// end::c48264ec5d9b9679fddd72e5c44425b9[]

			response0.MatchesExample(@"GET /_cluster/health/twitter?level=shards");
		}
	}
}
