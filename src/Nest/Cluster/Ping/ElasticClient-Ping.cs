using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	using Elasticsearch.Net.Connection.Configuration;
	using PingConverter = Func<IApiCallDetails, Stream, PingResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// Executes a HEAD request to the cluster to determine whether it's up or not.
		/// </summary>
		IPingResponse Ping(Func<PingDescriptor, IPingRequest> pingSelector = null);

		/// <inheritdoc/>
		Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> pingSelector = null);

		/// <inheritdoc/>
		IPingResponse Ping(IPingRequest pingRequest);

		/// <inheritdoc/>
		Task<IPingResponse> PingAsync(IPingRequest pingRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPingResponse Ping(Func<PingDescriptor, IPingRequest> pingSelector = null) =>
			this.Ping(pingSelector.InvokeOrDefault(new PingDescriptor()));

		/// <inheritdoc/>
		public Task<IPingResponse> PingAsync(Func<PingDescriptor, IPingRequest> pingSelector = null) =>
			this.PingAsync(pingSelector.InvokeOrDefault(new PingDescriptor()));

		/// <inheritdoc/>
		public IPingResponse Ping(IPingRequest pingRequest) => 
			this.Dispatcher.Dispatch<IPingRequest, PingRequestParameters, PingResponse>(
				SetPingTimeout(pingRequest),
				new PingConverter(DeserializePingResponse),
				(p, d) => this.LowLevelDispatch.PingDispatch<PingResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IPingResponse> PingAsync(IPingRequest pingRequest) => 
			this.Dispatcher.DispatchAsync<IPingRequest, PingRequestParameters, PingResponse, IPingResponse>(
				SetPingTimeout(pingRequest),
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
