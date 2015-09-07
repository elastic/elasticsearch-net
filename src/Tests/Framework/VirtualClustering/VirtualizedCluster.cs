using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using Nest;
using System;
using System.Threading.Tasks;
using Tests.Framework.MockData;
using Elasticsearch.Net.Connection;

namespace Tests.Framework
{
	public class VirtualizedCluster
	{
		private ElasticClient _client;
		private readonly VirtualCluster _cluster;
		private readonly IConnectionPool _connectionPool;
		private readonly TestableDateTimeProvider _dateTimeProvider;
		private FixedPipelineFactory _fixedRequestPipeline;

		public IConnectionPool ConnectionPool => this._client.ConnectionSettings.ConnectionPool;

		public VirtualizedCluster(VirtualCluster cluster, IConnectionPool pool, ConnectionSettings settings)
		{
			this._dateTimeProvider = new TestableDateTimeProvider();
			this._fixedRequestPipeline = new FixedPipelineFactory(settings, this._dateTimeProvider);
			this._client = this._fixedRequestPipeline.Client;

			this._cluster = cluster;
			this._connectionPool = pool;
		}

		public ISearchResponse<Project> ClientCall() => this._client.Search<Project>(s => s);

		public async Task<ISearchResponse<Project>> ClientCallAsync() => await this._client.SearchAsync<Project>(s => s);

		public void ChangeTime(Func<DateTime, DateTime> change) => change(_dateTimeProvider.MutableNow);
	}

}