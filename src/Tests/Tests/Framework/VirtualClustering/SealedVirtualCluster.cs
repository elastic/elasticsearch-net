using System;
using Elasticsearch.Net;
using Nest;

namespace Tests.Framework
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

		private ConnectionSettings CreateSettings() =>
			new ConnectionSettings(_connectionPool, _connection).DefaultIndex("default-index");

		public VirtualizedCluster AllDefaults() =>
			new VirtualizedCluster(_dateTimeProvider, CreateSettings());

		public VirtualizedCluster Settings(Func<ConnectionSettings, ConnectionSettings> selector) =>
			new VirtualizedCluster(_dateTimeProvider, selector(CreateSettings()));
	}
}
