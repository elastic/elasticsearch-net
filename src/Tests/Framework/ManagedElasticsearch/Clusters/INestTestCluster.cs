using Nest;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	public interface INestTestCluster
	{
		IElasticClient Client { get; }
	}
}