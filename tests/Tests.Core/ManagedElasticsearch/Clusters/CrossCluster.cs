// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Tests.Core.ManagedElasticsearch.NodeSeeders;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class CrossCluster : ClientTestClusterBase
	{
		protected override void SeedNode()
		{
			new DefaultSeeder(Client).SeedNode();

			// persist settings for cross cluster search, when cluster_two is not available
			Client.Cluster.PutSettings(s => s
				.Persistent(d => d
					.Add("cluster.remote.cluster_two.seeds", new [] { "127.0.0.1:9399" })
					.Add("cluster.remote.cluster_two.skip_unavailable", true)
				)
			);
		}
	}
}
