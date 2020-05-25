// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Upgrade
{
	public class ClusterRestartPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("upgrade/cluster_restart.asciidoc:32")]
		public void Line32()
		{
			// tag::1cd3b9d65576a9212eef898eb3105758[]
			var response0 = new SearchResponse<object>();
			// end::1cd3b9d65576a9212eef898eb3105758[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster.routing.allocation.enable"": ""primaries""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("upgrade/cluster_restart.asciidoc:31")]
		public void Line31()
		{
			// tag::f27c28ddbf4c266b5f42d14da837b8de[]
			var response0 = new SearchResponse<object>();
			// end::f27c28ddbf4c266b5f42d14da837b8de[]

			response0.MatchesExample(@"POST /_flush");
		}

		[U(Skip = "Example not implemented")]
		[Description("upgrade/cluster_restart.asciidoc:66")]
		public void Line66()
		{
			// tag::a21a7bf052b41f5b996dc58f7b69770f[]
			var response0 = new SearchResponse<object>();
			// end::a21a7bf052b41f5b996dc58f7b69770f[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=true");
		}

		[U(Skip = "Example not implemented")]
		[Description("upgrade/cluster_restart.asciidoc:91")]
		public void Line91()
		{
			// tag::c0a4b0c1c6eff14da8b152ceb19c1c31[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::c0a4b0c1c6eff14da8b152ceb19c1c31[]

			response0.MatchesExample(@"GET _cat/health");

			response1.MatchesExample(@"GET _cat/nodes");
		}

		[U(Skip = "Example not implemented")]
		[Description("upgrade/cluster_restart.asciidoc:124")]
		public void Line124()
		{
			// tag::45ef5156dbd2d3fd4fd22b8d99f7aad4[]
			var response0 = new SearchResponse<object>();
			// end::45ef5156dbd2d3fd4fd22b8d99f7aad4[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster.routing.allocation.enable"": null
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("upgrade/cluster_restart.asciidoc:143")]
		public void Line143()
		{
			// tag::2d9b30acd6b5683f39d53494c0dd779c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2d9b30acd6b5683f39d53494c0dd779c[]

			response0.MatchesExample(@"GET _cat/health");

			response1.MatchesExample(@"GET _cat/recovery");
		}

		[U(Skip = "Example not implemented")]
		[Description("upgrade/cluster_restart.asciidoc:159")]
		public void Line159()
		{
			// tag::3c5d5a5c34a62724942329658c688f5e[]
			var response0 = new SearchResponse<object>();
			// end::3c5d5a5c34a62724942329658c688f5e[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=false");
		}
	}
}
