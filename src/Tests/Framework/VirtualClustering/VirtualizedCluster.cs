using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public class VirtualizedCluster
	{
		private ElasticClient Client => this._fixedRequestPipeline?.Client;
		private readonly VirtualCluster _cluster;
		private readonly IConnectionPool _connectionPool;
		private readonly TestableDateTimeProvider _dateTimeProvider;
		private readonly ConnectionSettings _settings;
		public FixedPipelineFactory _fixedRequestPipeline;

		public IConnectionPool ConnectionPool => this.Client.ConnectionSettings.ConnectionPool;

		public VirtualizedCluster(VirtualCluster cluster, IConnectionPool pool, TestableDateTimeProvider dateTimeProvider, ConnectionSettings settings)
		{
			this._dateTimeProvider = dateTimeProvider;
			this._settings = settings;
			this._fixedRequestPipeline = new FixedPipelineFactory(settings, this._dateTimeProvider);

			this._cluster = cluster;
			this._connectionPool = pool;
		}

		public ISearchResponse<Project> ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) =>
			this.Client.Search<Project>(s => s.RequestConfiguration(requestOverrides));

		public async Task<ISearchResponse<Project>> ClientCallAsync(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) => 
			await this.Client.SearchAsync<Project>(s => s.RequestConfiguration(requestOverrides));

		public void ChangeTime(Func<DateTime, DateTime> change) => _dateTimeProvider.ChangeTime(change);

		public void ClientThrows(bool throws) => _settings.ThrowExceptions(throws);
	}

}