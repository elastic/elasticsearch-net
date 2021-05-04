// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
 using Elastic.Elasticsearch.Managed.Configuration;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	/// <summary> Cluster that modifies the state of the Watcher Service</summary>
	public class WatcherStateCluster : XPackCluster
	{
		protected override void ModifyNodeConfiguration(NodeConfiguration n, int port) => n.WaitForShutdown = TimeSpan.FromSeconds(30);
	}
}
