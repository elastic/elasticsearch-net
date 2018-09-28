using Tests.Core.ManagedElasticsearch.NodeSeeders;
using static Elastic.Managed.Ephemeral.Plugins.ElasticsearchPlugin;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class ReadOnlyCluster : ClientTestClusterBase
	{
		public ReadOnlyCluster() : base(MapperMurmur3, AnalysisKuromoji, AnalysisIcu, AnalysisPhonetic) { }

		protected override void SeedCluster() => new DefaultSeeder(this.Client).SeedNode();
	}
}
