using Xunit;

namespace Tests.Framework.Integration
{
	/// <summary>
	/// Use this cluster for api's that do writes. If they are however intrusive or long running consider IntrusiveOperationCluster instead.
	/// </summary>
	[RequiresPlugin(ElasticsearchPlugin.MapperAttachments, ElasticsearchPlugin.IngestGeoIp, ElasticsearchPlugin.IngestAttachment)]
	public class WritableCluster : ClusterBase
	{
		public override int MaxConcurrency => 4;


		public override void Bootstrap()
		{
			var seeder = new Seeder(this.Node);
			seeder.SeedNode();
		}
	}
}
