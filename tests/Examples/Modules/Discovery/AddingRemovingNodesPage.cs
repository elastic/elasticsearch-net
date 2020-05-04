// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Modules.Discovery
{
	public class AddingRemovingNodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/discovery/adding-removing-nodes.asciidoc:64")]
		public void Line64()
		{
			// tag::4e1f02928ef243bf07fd425754b7642b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::4e1f02928ef243bf07fd425754b7642b[]

			response0.MatchesExample(@"# Add node to voting configuration exclusions list and wait for the system
			# to auto-reconfigure the node out of the voting configuration up to the
			# default timeout of 30 seconds");

			response1.MatchesExample(@"POST /_cluster/voting_config_exclusions?node_names=node_name");

			response2.MatchesExample(@"# Add node to voting configuration exclusions list and wait for
			# auto-reconfiguration up to one minute");

			response3.MatchesExample(@"POST /_cluster/voting_config_exclusions?node_names=node_name&timeout=1m");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/discovery/adding-removing-nodes.asciidoc:107")]
		public void Line107()
		{
			// tag::92f073762634a4b2274f71002494192e[]
			var response0 = new SearchResponse<object>();
			// end::92f073762634a4b2274f71002494192e[]

			response0.MatchesExample(@"GET /_cluster/state?filter_path=metadata.cluster_coordination.voting_config_exclusions");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/discovery/adding-removing-nodes.asciidoc:125")]
		public void Line125()
		{
			// tag::ead4d875877d618594d0cdbdd9b7998b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::ead4d875877d618594d0cdbdd9b7998b[]

			response0.MatchesExample(@"# Wait for all the nodes with voting configuration exclusions to be removed from
			# the cluster and then remove all the exclusions, allowing any node to return to
			# the voting configuration in the future.");

			response1.MatchesExample(@"DELETE /_cluster/voting_config_exclusions");

			response2.MatchesExample(@"# Immediately remove all the voting configuration exclusions, allowing any node
			# to return to the voting configuration in the future.");

			response3.MatchesExample(@"DELETE /_cluster/voting_config_exclusions?wait_for_removal=false");
		}
	}
}