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
		/// Executes a HEAD request to the cluster to determine whether it's up or not.
		/// </summary>
		IPingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null);

		/// <inheritdoc />
		Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		IPingResponse Ping(IPingRequest request);

		/// <inheritdoc />
		Task<IPingResponse> PingAsync(IPingRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null) =>
			Ping(selector.InvokeOrDefault(new PingDescriptor()));

		/// <inheritdoc />
		public IPingResponse Ping(IPingRequest request) =>
			DoRequest<IPingRequest, PingResponse>(request, request.RequestParameters, r => SetPingTimeout(r));

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null, CancellationToken ct = default) =>
			PingAsync(selector.InvokeOrDefault(new PingDescriptor()), ct);

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(IPingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPingRequest, IPingResponse, PingResponse>(request, request.RequestParameters, ct, r => SetPingTimeout(r));

		private void SetPingTimeout(IRequestConfiguration requestConfiguration)
		{
			if (!ConnectionSettings.PingTimeout.HasValue) return;
			requestConfiguration.RequestTimeout = ConnectionSettings.PingTimeout.Value;
		}
	}
}
