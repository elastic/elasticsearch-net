using Elastic.Managed.Ephemeral.Plugins;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	/// <summary>
	/// Use this cluster for api's that do writes. If they are however intrusive or long running consider IntrusiveOperationCluster instead.
	/// </summary>
	public class WritableCluster : ClientTestClusterBase
	{
		public WritableCluster() : base(new ClientTestClusterConfiguration
		{
			MaxConcurrency = 4
		})
		{
			this.Plugins.Add(ElasticsearchPlugin.IngestGeoIp);
			this.Plugins.Add(ElasticsearchPlugin.AnalysisKuromoji);
			this.Plugins.Add(ElasticsearchPlugin.AnalysisIcu);
			this.Plugins.Add(ElasticsearchPlugin.IngestAttachment);
		}

		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(this.Client);
			seeder.SeedNode();
		}
	}
}
