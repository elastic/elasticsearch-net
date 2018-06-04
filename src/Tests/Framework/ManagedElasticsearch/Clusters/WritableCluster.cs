using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using static Elastic.Managed.Ephemeral.Plugins.ElasticsearchPlugin;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	/// <summary> Use this cluster for api's that do writes. If they are however intrusive or long running consider IntrusiveOperationCluster instead. </summary>
	public class WritableCluster : ClientTestClusterBase
	{
		public WritableCluster() : base(new ClientTestClusterConfiguration(
			IngestGeoIp, IngestAttachment, AnalysisKuromoji, AnalysisIcu, AnalysisPhonetic, MapperMurmur3
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
