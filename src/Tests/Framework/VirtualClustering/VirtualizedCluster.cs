using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Providers;
using Nest;
using System;
using System.Threading.Tasks;
using Tests.Framework.MockData;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;

namespace Tests.Framework
{
	public class VirtualizedCluster
	{
		private ElasticClient _client;
		private readonly VirtualCluster _cluster;
		private readonly IConnectionPool _connectionPool;
		private readonly TestableDateTimeProvider _dateTimeProvider;
		public FixedPipelineFactory _fixedRequestPipeline;

		public IConnectionPool ConnectionPool => this._client.ConnectionSettings.ConnectionPool;

		public VirtualizedCluster(VirtualCluster cluster, IConnectionPool pool, TestableDateTimeProvider dateTimeProvider, ConnectionSettings settings)
		{
			this._dateTimeProvider = dateTimeProvider;
			this._fixedRequestPipeline = new FixedPipelineFactory(settings, this._dateTimeProvider);
			this._client = this._fixedRequestPipeline.Client;

			this._cluster = cluster;
			this._connectionPool = pool;
		}

		public ISearchResponse<Project> ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) 
			=> this._client.Search<Project>(s => s.RequestConfiguration(requestOverrides));

		public async Task<ISearchResponse<Project>> ClientCallAsync(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) => 
			await this._client.SearchAsync<Project>(s => s.RequestConfiguration(requestOverrides));

		public void ChangeTime(Func<DateTime, DateTime> change) => _dateTimeProvider.ChangeTime(change);
	}

}