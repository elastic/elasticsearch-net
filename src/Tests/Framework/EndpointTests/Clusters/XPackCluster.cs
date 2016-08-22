using Tests.Framework.Integration;
using Xunit;

namespace Tests.Framework.Integration
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackCluster : ClusterBase
	{
	}
}
