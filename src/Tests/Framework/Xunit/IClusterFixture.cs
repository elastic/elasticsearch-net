using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Xunit
{
	public interface IClusterFixture<TFixture> : IClassFixture<EndpointUsage>
		where TFixture : ClusterBase, new()
	{ }
}
