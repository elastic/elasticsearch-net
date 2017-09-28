using System.Collections.Generic;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public class UnbalancedCluster : ReadOnlyCluster
	{
		protected override void SeedNode() =>
			new DefaultSeeder(this.Node, new IndexSettings(new Dictionary<string, object> {
				//{ "mapping.single_type", "false" } //TODO this is temporarily while parent child mappings are reimagined in 6.0
			}) { NumberOfShards = 3, NumberOfReplicas = 2 }).SeedNode();
	}
}
