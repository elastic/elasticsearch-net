using System;
using System.IO;
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

		/// <inheritdoc/>
		Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null);

		/// <inheritdoc/>
		IPingResponse Ping(IPingRequest request);

		/// <inheritdoc/>
		Task<IPingResponse> PingAsync(IPingRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPingResponse Ping(Func<PingDescriptor, IPingRequest> selector = null) =>
			this.Ping(selector.InvokeOrDefault(new PingDescriptor()));

		/// <inheritdoc/>
		public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> selector = null) =>
			this.PingAsync(selector.InvokeOrDefault(new PingDescriptor()));

		/// <inheritdoc/>
		public IPingResponse Ping(IPingRequest request) => 
			this.Dispatcher.Dispatch<IPingRequest, PingRequestParameters, PingResponse>(
				SetPingTimeout(request),
				new PingConverter(DeserializePingResponse),
				(p, d) => this.LowLevelDispatch.PingDispatch<PingResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IPingResponse> PingAsync(IPingRequest request) => 
			this.Dispatcher.DispatchAsync<IPingRequest, PingRequestParameters, PingResponse, IPingResponse>(
				SetPingTimeout(request),
				new PingConverter(DeserializePingResponse),
				(p, d) => this.LowLevelDispatch.PingDispatchAsync<PingResponse>(p)
			);

		private IPingRequest SetPingTimeout(IPingRequest pingRequest)
		{
			if (!this.ConnectionSettings.PingTimeout.HasValue) return pingRequest;
			var timeout = this.ConnectionSettings.PingTimeout.Value;
			return this.ForceConfiguration<IPingRequest, PingRequestParameters>(pingRequest, r => r.RequestTimeout = timeout);
		}

		private PingResponse DeserializePingResponse(IApiCallDetails response, Stream stream) => new PingResponse();
	}
}
