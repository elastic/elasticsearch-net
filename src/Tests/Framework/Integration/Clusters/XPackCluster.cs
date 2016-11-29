using System;
using Nest;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Framework.Integration
{
	public class XPackCluster : ClusterBase
	{
		protected override bool EnableShield => true;

		protected override bool EnableWatcher => true;

		public override void Bootstrap()
		{
			var startWatcherResponse = this.Client.StartWatcher();
			this.WaitForWatcherToStart();

			base.Bootstrap();
		}

		private void WaitForWatcherToStart()
		{
			try
			{
				var watcherInfoResponse = this.Client.WatcherStats();
				if (!watcherInfoResponse.IsValid || 
					watcherInfoResponse.WatcherState != WatcherState.Started)
				{
					WaitForWatcherToStart();
				}
			}
			catch (Exception)
			{
				WaitForWatcherToStart();
			}
		}
	}

	/// <summary>
	/// Cluster that modifies the state of the Watcher Service
	/// </summary>
	public class WatcherStateCluster : XPackCluster
	{
	}
}
