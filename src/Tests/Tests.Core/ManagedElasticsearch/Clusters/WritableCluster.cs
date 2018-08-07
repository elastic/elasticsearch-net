using Elastic.Managed.Ephemeral.Plugins;
using Tests.Core.ManagedElasticsearch.NodeSeeders;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	/// <summary> Use this cluster for api's that do writes. If they are however intrusive or long running consider IntrusiveOperationCluster instead. </summary>
	public class WritableCluster : ClientTestClusterBase
	{
		public WritableCluster() : base(new ClientTestClusterConfiguration(
			ElasticsearchPlugin.IngestGeoIp, ElasticsearchPlugin.IngestAttachment, ElasticsearchPlugin.AnalysisKuromoji, ElasticsearchPlugin.AnalysisIcu, ElasticsearchPlugin.AnalysisPhonetic, ElasticsearchPlugin.MapperMurmur3
		)
		{
			MaxConcurrency = 4
		}) { }

		protected override void SeedCluster()
		{
			var seeder = new DefaultSeeder(this.Client);
			seeder.SeedNode();
		}
	}
}
