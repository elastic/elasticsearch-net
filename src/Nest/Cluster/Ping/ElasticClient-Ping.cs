using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;
	using PingConverter = Func<IApiCallDetails, Stream, PingResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Executes a HEAD request to the cluster to determine whether it's up or not.
		/// </summary>
		IPingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null);

		/// <inheritdoc/>
		Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		IPingResponse Ping(IPingRequest request);

		/// <inheritdoc/>
		Task<IPingResponse> PingAsync(IPingRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null) =>
			this.Ping(selector.InvokeOrDefault(new PingDescriptor()));

		/// <inheritdoc/>
		public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PingAsync(selector.InvokeOrDefault(new PingDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public IPingResponse Ping(IPingRequest request) =>
			this.Dispatcher.Dispatch<IPingRequest, PingRequestParameters, PingResponse>(
				SetPingTimeout(request),
				(p, d) => this.LowLevelDispatch.PingDispatch<PingResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IPingResponse> PingAsync(IPingRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPingRequest, PingRequestParameters, PingResponse, IPingResponse>(
				SetPingTimeout(request),
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.PingDispatchAsync<PingResponse>(p, c)
			);

		private IPingRequest SetPingTimeout(IPingRequest pingRequest)
		{
			if (!this.ConnectionSettings.PingTimeout.HasValue) return pingRequest;
			var timeout = this.ConnectionSettings.PingTimeout.Value;
			return ForceConfiguration<IPingRequest, PingRequestParameters>(pingRequest, r => r.RequestTimeout = timeout);
		}
	}
}
