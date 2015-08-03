using Elasticsearch.Net.ConnectionPool;
using Nest;
using System;

namespace Tests.ClientConcepts.LowLevel.Ressurection
{
	public class SealedVirtualCluster
	{
		private readonly VirtualCluster _cluster;
		private readonly IConnectionPool _connectionPool;
		public SealedVirtualCluster(VirtualCluster cluster, IConnectionPool pool)
		{
			this._cluster = cluster;
			this._connectionPool = pool;
		}

		public VirtualizedCluster Settings(Func<ConnectionSettings, ConnectionSettings> selector)
		{
			return new VirtualizedCluster(this._cluster, this._connectionPool, selector(new ConnectionSettings(this._connectionPool)));
		}
	}
}