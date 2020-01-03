using Nest;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public interface INestTestCluster
	{
		IElasticClient Client { get; }
	}
}
