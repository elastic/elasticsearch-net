using Tests.Framework.Integration;

namespace Xunit
{
	public interface IClusterFixture<TFixture> : IClassFixture<EndpointUsage>
		where TFixture : ClusterBase, new()
	{ }
}
