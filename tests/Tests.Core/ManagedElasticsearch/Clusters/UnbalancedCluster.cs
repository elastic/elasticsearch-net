namespace Tests.Core.ManagedElasticsearch.Clusters
{
	//TODO does this need a whole separate cluster? Re-use read-only?
	public class UnbalancedCluster : ClientTestClusterBase
	{
		//protected override void SeedNode() =>
		//	new DefaultSeeder(Client, new IndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 })
		//		.SeedNode();
	}
}
