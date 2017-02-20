using Tests.Framework.Integration;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	/// <summary>
	/// Use this cluster for heavy API's, either on ES's side or the client (intricate setup etc)
	/// </summary>
	public class IntrusiveOperationCluster : ClusterBase
	{
		public override int MaxConcurrency => 1;
	}
}
