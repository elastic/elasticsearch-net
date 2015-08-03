using Elasticsearch.Net.ConnectionPool;
using Nest;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.LowLevel.Ressurection
{
	public class VirtualizedCluster
	{
		private ElasticClient _client;
		private readonly VirtualCluster _cluster;
		private readonly IConnectionPool _connectionPool;

		public VirtualizedCluster(VirtualCluster cluster, IConnectionPool pool, ConnectionSettings settings)
		{
			this._cluster = cluster;
			this._connectionPool = pool;
			//TODO inject IConnection that takes virtual cluster into account
			this._client = new ElasticClient(settings);
		}

		public ISearchResponse<Project> ClientCall()
		{
			return this._client.Search<Project>(s => s);
		}
	}
}