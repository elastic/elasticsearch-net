using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	using PingConverter = Func<IApiCallDetails, Stream, PingResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPingResponse Ping(Func<PingDescriptor, PingDescriptor> pingSelector = null)
		{
			pingSelector = pingSelector ?? (s => s);
			return this.Dispatcher.Dispatch<PingDescriptor, PingRequestParameters, PingResponse>(
				pingSelector,
				(p, d) => 
				{	
					SetRequestTimeout(d);
					return this.LowLevelDispatch.PingDispatch<PingResponse>(
						p.DeserializationState(new PingConverter(DeserializePingResponse))
					);
				}

			);
		}

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(Func<PingDescriptor, PingDescriptor> pingSelector = null)
		{
			pingSelector = pingSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<PingDescriptor, PingRequestParameters, PingResponse, IPingResponse>(
				pingSelector,
				(p, d) =>
				{
					SetRequestTimeout(d);
					return this.LowLevelDispatch.PingDispatchAsync<PingResponse>(
					   p.DeserializationState(new PingConverter(DeserializePingResponse))
					);
				}
			);
		}

		/// <inheritdoc />
		public IPingResponse Ping(IPingRequest pingRequest)
		{
			return this.Dispatcher.Dispatch<IPingRequest, PingRequestParameters, PingResponse>(
				pingRequest,
				(p, d) =>
				{
					SetRequestTimeout(d);
					return this.LowLevelDispatch.PingDispatch<PingResponse>(
						p.DeserializationState(new PingConverter(DeserializePingResponse))
					);
				}
			);
		}

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(IPingRequest pingRequest)
		{
			return this.Dispatcher.DispatchAsync<IPingRequest, PingRequestParameters, PingResponse, IPingResponse>(
				pingRequest,
				(p, d) =>
				{
					SetRequestTimeout(d);
					return this.LowLevelDispatch.PingDispatchAsync<PingResponse>(
						p.DeserializationState(new PingConverter(DeserializePingResponse))
					);
				}
			);
		}

		private void SetRequestTimeout(IRequest<PingRequestParameters> pingRequest)
		{
			if (this.ConnectionSettings.PingTimeout.HasValue)
				pingRequest.RequestConfiguration.RequestTimeout = this.ConnectionSettings.PingTimeout.Value;
		}

		private PingResponse DeserializePingResponse(IApiCallDetails response, Stream stream) => 
			new PingResponse();
	}
}
