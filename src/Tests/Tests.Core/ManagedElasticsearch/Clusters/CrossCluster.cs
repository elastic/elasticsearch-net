using Tests.Core.ManagedElasticsearch.NodeSeeders;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	/// <summary>
	/// Cluster used for Cross cluster search, Cross cluster replication
	/// </summary>
	public class CrossCluster : ClientTestClusterBase
	{
		public static string ClusterTwo = "cluster_two";

		protected override void SeedCluster()
		{
			new DefaultSeeder(Client).SeedNode();

			// persist settings for cross cluster search, when remote-cluster is not available
			Client.ClusterPutSettings(s => s
				.Persistent(d => d
					.Add($"cluster.remote.{ClusterTwo}.seeds", new [] { "127.0.0.1:9399" })
					.Add($"cluster.remote.{ClusterTwo}.skip_unavailable", true)
				)
			);
		}
	}
}
