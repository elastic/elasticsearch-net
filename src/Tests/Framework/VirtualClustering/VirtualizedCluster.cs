using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public class VirtualizedCluster
	{
		private readonly ElasticClient _client;
		private readonly VirtualCluster _cluster;
		private readonly IConnectionPool _connectionPool;
		private readonly TestableDateTimeProvider _dateTimeProvider;
		private readonly ConnectionSettings _settings;
		public FixedPipelineFactory _fixedRequestPipeline;

		public IConnectionPool ConnectionPool => this._client.ConnectionSettings.ConnectionPool;

		public VirtualizedCluster(VirtualCluster cluster, IConnectionPool pool, TestableDateTimeProvider dateTimeProvider, ConnectionSettings settings)
		{
			this._dateTimeProvider = dateTimeProvider;
			_settings = settings;
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

		public void ClientThrows(bool throws) => _settings.ThrowExceptions(throws);
	}

}