using Elastic.Managed.Ephemeral.Plugins;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using static Elastic.Stack.Artifacts.Products.ElasticsearchPlugin;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	/// <summary>
	/// Use this cluster for heavy API's, either on ES's side or the client (intricate setup etc)
	/// </summary>
	public class IntrusiveOperationSeededCluster : ClientTestClusterBase
	{
		public IntrusiveOperationSeededCluster() : base(new ClientTestClusterConfiguration(IngestGeoIp, IngestAttachment)
		{
			MaxConcurrency = 1
		}) { }

		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(Client);
			seeder.SeedNode();
		}
	}
}
