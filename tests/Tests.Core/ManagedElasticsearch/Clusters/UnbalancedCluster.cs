// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Core.ManagedElasticsearch.NodeSeeders;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	//TODO does this need a whole separate cluster?
	public class UnbalancedCluster : ClientTestClusterBase
	{
		protected override void SeedNode() =>
			new DefaultSeeder(Client, new IndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 })
				.SeedNode();
	}
}
