// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cluster
{
	public class NodesStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-stats.asciidoc:2152")]
		public void Line2152()
		{
			// tag::5457c94f0039c6b95c7f9f305d0c6b58[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::5457c94f0039c6b95c7f9f305d0c6b58[]

			response0.MatchesExample(@"GET /_nodes/stats/indices");

			response1.MatchesExample(@"GET /_nodes/stats/os,process");

			response2.MatchesExample(@"GET /_nodes/10.0.0.1/stats/process");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-stats.asciidoc:2170")]
		public void Line2170()
		{
			// tag::150b5fee5678bf8cdf0932da73eada80[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::150b5fee5678bf8cdf0932da73eada80[]

			response0.MatchesExample(@"GET /_nodes/stats/indices/fielddata?fields=field1,field2");

			response1.MatchesExample(@"GET /_nodes/stats/indices/fielddata?level=indices&fields=field1,field2");

			response2.MatchesExample(@"GET /_nodes/stats/indices/fielddata?level=shards&fields=field1,field2");

			response3.MatchesExample(@"GET /_nodes/stats/indices/fielddata?fields=field*");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-stats.asciidoc:2188")]
		public void Line2188()
		{
			// tag::bd68666ca2e0be12f7624016317a62bc[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::bd68666ca2e0be12f7624016317a62bc[]

			response0.MatchesExample(@"GET /_nodes/stats?groups=_all");

			response1.MatchesExample(@"GET /_nodes/stats/indices?groups=foo,bar");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-stats.asciidoc:2204")]
		public void Line2204()
		{
			// tag::09769561f082b50558fb7d8707719963[]
			var response0 = new SearchResponse<object>();
			// end::09769561f082b50558fb7d8707719963[]

			response0.MatchesExample(@"GET /_nodes/stats/ingest?filter_path=nodes.*.ingest");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-stats.asciidoc:2212")]
		public void Line2212()
		{
			// tag::ef9c29759459904fef162acd223462c4[]
			var response0 = new SearchResponse<object>();
			// end::ef9c29759459904fef162acd223462c4[]

			response0.MatchesExample(@"GET /_nodes/stats?metric=ingest&filter_path=nodes.*.ingest");
		}

		[U(Skip = "Example not implemented")]
		[Description("cluster/nodes-stats.asciidoc:2220")]
		public void Line2220()
		{
			// tag::f160561efab38e40c2feebf5a2542ab5[]
			var response0 = new SearchResponse<object>();
			// end::f160561efab38e40c2feebf5a2542ab5[]

			response0.MatchesExample(@"GET /_nodes/stats?metric=ingest&filter_path=nodes.*.ingest.pipelines");
		}
	}
}
