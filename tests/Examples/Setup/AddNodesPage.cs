// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Setup
{
	public class AddNodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("setup/add-nodes.asciidoc:111")]
		public void Line111()
		{
			// tag::4e1f02928ef243bf07fd425754b7642b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::4e1f02928ef243bf07fd425754b7642b[]

			response0.MatchesExample(@"POST /_cluster/voting_config_exclusions?node_names=node_name");

			response1.MatchesExample(@"POST /_cluster/voting_config_exclusions?node_names=node_name&timeout=1m");
		}

		[U(Skip = "Example not implemented")]
		[Description("setup/add-nodes.asciidoc:154")]
		public void Line154()
		{
			// tag::92f073762634a4b2274f71002494192e[]
			var response0 = new SearchResponse<object>();
			// end::92f073762634a4b2274f71002494192e[]

			response0.MatchesExample(@"GET /_cluster/state?filter_path=metadata.cluster_coordination.voting_config_exclusions");
		}

		[U(Skip = "Example not implemented")]
		[Description("setup/add-nodes.asciidoc:172")]
		public void Line172()
		{
			// tag::ead4d875877d618594d0cdbdd9b7998b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::ead4d875877d618594d0cdbdd9b7998b[]

			response0.MatchesExample(@"DELETE /_cluster/voting_config_exclusions");

			response1.MatchesExample(@"DELETE /_cluster/voting_config_exclusions?wait_for_removal=false");
		}
	}
}
