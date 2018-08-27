using System;
using System.Threading;
using Elastic.Managed.Configuration;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	/// <summary>
	/// Cluster that modifies the state of the Watcher Service
	/// </summary>
	public class WatcherStateCluster : XPackCluster
	{
		protected override void ModifyNodeConfiguration(NodeConfiguration n, int port)
		{
			n.WaitForShutdown = TimeSpan.FromSeconds(30);
		}
	}
}
