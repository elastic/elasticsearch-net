using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	using PingConverter = Func<IElasticsearchResponse, Stream, PingResponse>;

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPingResponse Ping(Func<PingDescriptor, PingDescriptor> pingSelector = null)
		{
			pingSelector = pingSelector ?? (s => s);
			return this.Dispatch<PingDescriptor, PingRequestParameters, PingResponse>(
				pingSelector,
				(p, d) => 
				{	
					SetRequestTimeout(d);
					return this.RawDispatch.PingDispatch<PingResponse>(
						p.DeserializationState(new PingConverter(DeserializePingResponse))
					);
				}

			);
		}

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(Func<PingDescriptor, PingDescriptor> pingSelector = null)
		{
			pingSelector = pingSelector ?? (s => s);
			return this.DispatchAsync<PingDescriptor, PingRequestParameters, PingResponse, IPingResponse>(
				pingSelector,
				(p, d) =>
				{
					SetRequestTimeout(d);
					return this.RawDispatch.PingDispatchAsync<PingResponse>(
					   p.DeserializationState(new PingConverter(DeserializePingResponse))
					);
				}
			);
		}

		/// <inheritdoc />
		public IPingResponse Ping(IPingRequest pingRequest)
		{
			return this.Dispatch<IPingRequest, PingRequestParameters, PingResponse>(
				pingRequest,
				(p, d) =>
				{
					SetRequestTimeout(d);
					return this.RawDispatch.PingDispatch<PingResponse>(
						p.DeserializationState(new PingConverter(DeserializePingResponse))
					);
				}
			);
		}

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(IPingRequest pingRequest)
		{
			return this.DispatchAsync<IPingRequest, PingRequestParameters, PingResponse, IPingResponse>(
				pingRequest,
				(p, d) =>
				{
					SetRequestTimeout(d);
					return this.RawDispatch.PingDispatchAsync<PingResponse>(
						p.DeserializationState(new PingConverter(DeserializePingResponse))
					);
				}
			);
		}

		private void SetRequestTimeout(IRequest<PingRequestParameters> pingRequest)
		{
			if (this._connectionSettings.PingTimeout.HasValue)
				pingRequest.RequestConfiguration.RequestTimeout = this._connectionSettings.PingTimeout.Value;
		}

		private PingResponse DeserializePingResponse(IElasticsearchResponse response, Stream stream)
		{
			return new PingResponse
			{
				IsValid = response.Success && response.HttpStatusCode == 200
			};
		}
	}
}
