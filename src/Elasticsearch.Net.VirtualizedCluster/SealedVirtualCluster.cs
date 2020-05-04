// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net.VirtualizedCluster.Providers;

namespace Elasticsearch.Net.VirtualizedCluster
{
	public class SealedVirtualCluster
	{
		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly TestableDateTimeProvider _dateTimeProvider;

		public SealedVirtualCluster(VirtualCluster cluster, IConnectionPool pool, TestableDateTimeProvider dateTimeProvider)
		{
			_connectionPool = pool;
			_connection = new VirtualClusterConnection(cluster, dateTimeProvider);
			_dateTimeProvider = dateTimeProvider;
		}

		private ConnectionConfiguration CreateSettings() =>
			new ConnectionConfiguration(_connectionPool, _connection);

		public VirtualizedCluster AllDefaults() =>
			new VirtualizedCluster(_dateTimeProvider, CreateSettings());

		public VirtualizedCluster Settings(Func<ConnectionConfiguration, ConnectionConfiguration> selector) =>
			new VirtualizedCluster(_dateTimeProvider, selector(CreateSettings()));
		
		public VirtualClusterConnection VirtualClusterConnection(Func<ConnectionConfiguration, ConnectionConfiguration> selector = null) =>
			new VirtualizedCluster(_dateTimeProvider, selector == null ? CreateSettings() : selector(CreateSettings()))
				.Connection;
	}
}
