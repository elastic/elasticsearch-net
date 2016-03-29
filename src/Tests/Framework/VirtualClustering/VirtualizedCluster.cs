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
		private Func<IElasticClient, Func<RequestConfigurationDescriptor, IRequestConfiguration>, IResponse> _syncCall;
		private Func<IElasticClient, Func<RequestConfigurationDescriptor, IRequestConfiguration>, Task<IResponse>> _asyncCall;

		public IConnectionPool ConnectionPool => this.Client.ConnectionSettings.ConnectionPool;

		public VirtualizedCluster(VirtualCluster cluster, IConnectionPool pool, TestableDateTimeProvider dateTimeProvider, ConnectionSettings settings)
		{
			this._dateTimeProvider = dateTimeProvider;
			this._settings = settings;
			this._fixedRequestPipeline = new FixedPipelineFactory(settings, this._dateTimeProvider);

			this._cluster = cluster;
			this._connectionPool = pool;

			this._syncCall = (c, r) => c.Search<Project>(s => s.RequestConfiguration(r));
			this._asyncCall = async (c, r) =>
			{
				var res = await c.SearchAsync<Project>(s => s.RequestConfiguration(r));
				return (IResponse)res;
			};
		}

		public VirtualizedCluster ClientProxiesTo(
			Func<IElasticClient, Func<RequestConfigurationDescriptor, IRequestConfiguration>, IResponse> sync,
			Func<IElasticClient, Func<RequestConfigurationDescriptor, IRequestConfiguration>, Task<IResponse>> async
			)
		{
			this._syncCall = sync;
			this._asyncCall = async;
			return this;
		}

		public IResponse ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) =>
			this._syncCall(this.Client, requestOverrides);

		public async Task<IResponse> ClientCallAsync(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) =>
			await this._asyncCall(this.Client, requestOverrides);

		public void ChangeTime(Func<DateTime, DateTime> change) => _dateTimeProvider.ChangeTime(change);

		public void ClientThrows(bool throws) => _settings.ThrowExceptions(throws);
	}

}
