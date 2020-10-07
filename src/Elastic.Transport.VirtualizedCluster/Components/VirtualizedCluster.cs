// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport.VirtualizedCluster.Products;
using Elastic.Transport.VirtualizedCluster.Providers;

namespace Elastic.Transport.VirtualizedCluster.Components
{
	public class VirtualizedCluster
	{
		private readonly FixedPipelineFactory _fixedRequestPipeline;
		private readonly TestableDateTimeProvider _dateTimeProvider;
		private readonly TransportConfiguration _settings;

		private Func<ITransport<ITransportConfigurationValues>, Func<RequestConfigurationDescriptor, IRequestConfiguration>, Task<ITransportResponse>> _asyncCall;
		private Func<ITransport<ITransportConfigurationValues>, Func<RequestConfigurationDescriptor, IRequestConfiguration>, ITransportResponse> _syncCall;

		private class VirtualResponse : TransportResponseBase { }

		internal VirtualizedCluster(TestableDateTimeProvider dateTimeProvider, TransportConfiguration settings)
		{
			_dateTimeProvider = dateTimeProvider;
			_settings = settings;
			_fixedRequestPipeline = new FixedPipelineFactory(settings, _dateTimeProvider);

			_syncCall = (t, r) => t.Request<VirtualResponse>(
				HttpMethod.GET, "/",
				PostData.Serializable(new {}), new RequestParameters(HttpMethod.GET, supportsBody: false)
			{
					RequestConfiguration = r?.Invoke(new RequestConfigurationDescriptor(null))
			});
			_asyncCall = async (t, r) =>
			{
				var res = await t.RequestAsync<VirtualResponse>
				(
					HttpMethod.GET, "/",
					CancellationToken.None,
					PostData.Serializable(new { }),
					new RequestParameters(HttpMethod.GET, supportsBody: false)
					{
						RequestConfiguration = r?.Invoke(new RequestConfigurationDescriptor(null))
					}
				).ConfigureAwait(false);
				return (ITransportResponse)res;
			};
		}

		public VirtualClusterConnection Connection => Client.Settings.Connection as VirtualClusterConnection;
		public IConnectionPool ConnectionPool => Client.Settings.ConnectionPool;
		public ITransport<ITransportConfigurationValues> Client => _fixedRequestPipeline?.Transport;

		public VirtualizedCluster TransportProxiesTo(
			Func<ITransport<ITransportConfigurationValues>, Func<RequestConfigurationDescriptor, IRequestConfiguration>, ITransportResponse> sync,
			Func<ITransport<ITransportConfigurationValues>, Func<RequestConfigurationDescriptor, IRequestConfiguration>, Task<ITransportResponse>> async
		)
		{
			_syncCall = sync;
			_asyncCall = async;
			return this;
		}

		public ITransportResponse ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) =>
			_syncCall(Client, requestOverrides);

		public async Task<ITransportResponse> ClientCallAsync(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides = null) =>
			await _asyncCall(Client, requestOverrides).ConfigureAwait(false);

		public void ChangeTime(Func<DateTime, DateTime> change) => _dateTimeProvider.ChangeTime(change);

		public void ClientThrows(bool throws) => _settings.ThrowExceptions(throws);
	}
}
