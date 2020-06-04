// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Modules.Discovery
{
	public class AddingRemovingNodesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/discovery/adding-removing-nodes.asciidoc:107")]
		public void Line107()
		{
			// tag::92f073762634a4b2274f71002494192e[]
			var response0 = new SearchResponse<object>();
			// end::92f073762634a4b2274f71002494192e[]

			response0.MatchesExample(@"GET /_cluster/state?filter_path=metadata.cluster_coordination.voting_config_exclusions");
		}
	}
}
