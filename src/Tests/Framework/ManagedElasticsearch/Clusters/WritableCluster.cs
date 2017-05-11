using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.Plugins;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	/// <summary>
	/// Use this cluster for api's that do writes. If they are however intrusive or long running consider IntrusiveOperationCluster instead.
	/// </summary>
	[RequiresPlugin(
		ElasticsearchPlugin.IngestGeoIp,
		ElasticsearchPlugin.AnalysisKuromoji,
		ElasticsearchPlugin.AnalysisIcu,
		ElasticsearchPlugin.IngestAttachment
	)]
	public class WritableCluster : ClusterBase
	{
		public override int MaxConcurrency => 4;

		protected override void SeedNode()
		{
			var seeder = new DefaultSeeder(this.Node);
			seeder.SeedNode();
		}
	}
}
