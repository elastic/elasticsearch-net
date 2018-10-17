using Elastic.Managed.Ephemeral.Plugins;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using static Elastic.Managed.Ephemeral.Plugins.ElasticsearchPlugin;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	/// <summary> Use this cluster for api's that do writes. If they are however intrusive or long running consider IntrusiveOperationCluster instead. </summary>
	public class WritableCluster : ClientTestClusterBase
	{
		public WritableCluster() : base(new ClientTestClusterConfiguration(
			IngestGeoIp,
			IngestAttachment,
			AnalysisKuromoji,
			AnalysisIcu,
			AnalysisPhonetic,
			MapperMurmur3,
			//TODO move this to elasticsearch-net abstractions
			new ElasticsearchPlugin("analysis-nori", v => v >= "6.4.0")
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
