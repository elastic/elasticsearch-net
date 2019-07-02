using System;
using Elasticsearch.Net.Virtual.Providers;

namespace Elasticsearch.Net.Virtual
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
	}
}
