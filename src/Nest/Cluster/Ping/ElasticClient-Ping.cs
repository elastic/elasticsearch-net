using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using PingConverter = Func<IApiCallDetails, Stream, PingResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		///     Executes a HEAD request to the cluster to determine whether it's up or not.
		/// </summary>
		IPingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null);

		/// <inheritdoc />
		IPingResponse Ping(IPingRequest request);

		/// <inheritdoc />
		Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IPingResponse> PingAsync(IPingRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null) =>
			Ping(selector.InvokeOrDefault(new PingDescriptor()));

		/// <inheritdoc />
		public IPingResponse Ping(IPingRequest request) =>
			Dispatcher.Dispatch<IPingRequest, PingRequestParameters, PingResponse>(
				SetPingTimeout(request),
				(p, d) => LowLevelDispatch.PingDispatch<PingResponse>(p)
			);

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PingAsync(selector.InvokeOrDefault(new PingDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(IPingRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IPingRequest, PingRequestParameters, PingResponse, IPingResponse>(
				SetPingTimeout(request),
				cancellationToken,
				(p, d, c) => LowLevelDispatch.PingDispatchAsync<PingResponse>(p, c)
			);

		private IPingRequest SetPingTimeout(IPingRequest pingRequest)
		{
			if (!ConnectionSettings.PingTimeout.HasValue) return pingRequest;

			var timeout = ConnectionSettings.PingTimeout.Value;
			return ForceConfiguration<IPingRequest, PingRequestParameters>(pingRequest, r => r.RequestTimeout = timeout);
		}
	}
}
