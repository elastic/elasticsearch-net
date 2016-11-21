using Tests.Framework.Integration;
using Xunit;

namespace Tests.Framework.Integration
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackCluster : ClusterBase
	{
	}

	/// <summary>
	/// Cluster that modifies the state of the Watcher Service
	/// </summary>
	public class WatcherStateCluster : XPackCluster
	{
	}
}
