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
		PingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null);

		/// <inheritdoc />
		Task<PingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		PingResponse Ping(IPingRequest request);

		/// <inheritdoc />
		Task<PingResponse> PingAsync(IPingRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null) =>
			Ping(selector.InvokeOrDefault(new PingDescriptor()));

		/// <inheritdoc />
		public PingResponse Ping(IPingRequest request) =>
			DoRequest<IPingRequest, PingResponse>(request, request.RequestParameters, r => SetPingTimeout(r));

		/// <inheritdoc />
		public Task<PingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null, CancellationToken ct = default) =>
			PingAsync(selector.InvokeOrDefault(new PingDescriptor()), ct);

		/// <inheritdoc />
		public Task<PingResponse> PingAsync(IPingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPingRequest, PingResponse, PingResponse>(request, request.RequestParameters, ct, r => SetPingTimeout(r));

		private void SetPingTimeout(IRequestConfiguration requestConfiguration)
		{
			if (!ConnectionSettings.PingTimeout.HasValue) return;
			requestConfiguration.RequestTimeout = ConnectionSettings.PingTimeout.Value;
		}
	}
}
