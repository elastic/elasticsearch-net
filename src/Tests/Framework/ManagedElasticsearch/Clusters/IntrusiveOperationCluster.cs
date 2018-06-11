using Elastic.Managed.Ephemeral;
using Elastic.Managed.Ephemeral.Plugins;
using Elastic.Xunit;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	/// <summary>
	/// Use this cluster for heavy API's, either on ES's side or the client (intricate setup etc)
	/// </summary>
	public class IntrusiveOperationCluster : ClientTestClusterBase
	{
		public IntrusiveOperationCluster() : base(new ClientTestClusterConfiguration(
			ElasticsearchPlugin.IngestGeoIp, ElasticsearchPlugin.IngestAttachment
		)
		{
			MaxConcurrency= 1
		}) { }
	}
}
