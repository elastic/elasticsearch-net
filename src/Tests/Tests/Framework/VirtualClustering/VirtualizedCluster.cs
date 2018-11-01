using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Domain;

namespace Tests.Framework
{
	public class VirtualizedCluster
	{
		private ElasticClient Client => _fixedRequestPipeline?.Client;
		private readonly TestableDateTimeProvider _dateTimeProvider;
		private readonly ConnectionSettings _settings;
		public FixedPipelineFactory _fixedRequestPipeline;
		private Func<IElasticClient, Func<RequestConfigurationDescriptor, IRequestConfiguration>, IResponse> _syncCall;
		private Func<IElasticClient, Func<RequestConfigurationDescriptor, IRequestConfiguration>, Task<IResponse>> _asyncCall;

		public IConnectionPool ConnectionPool => Client.ConnectionSettings.ConnectionPool;

		public VirtualizedCluster(TestableDateTimeProvider dateTimeProvider, ConnectionSettings settings)
		{
			_dateTimeProvider = dateTimeProvider;
			_settings = settings;
			_fixedRequestPipeline = new FixedPipelineFactory(settings, _dateTimeProvider);

			_syncCall = (c, r) => c.Search<Project>(s => s.RequestConfiguration(r));
			_asyncCall = async (c, r) =>
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
			_syncCall = sync;
			_asyncCall = async;
			return this;
		}

		public IResponse ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) =>
			_syncCall(Client, requestOverrides);

		public async Task<IResponse> ClientCallAsync(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) =>
			await _asyncCall(Client, requestOverrides);

		public void ChangeTime(Func<DateTime, DateTime> change) => _dateTimeProvider.ChangeTime(change);

		public void ClientThrows(bool throws) => _settings.ThrowExceptions(throws);
	}
}
