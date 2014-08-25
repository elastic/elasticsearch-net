using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPingResponse Ping(Func<PingDescriptor, PingDescriptor> pingSelector = null)
		{
			pingSelector = pingSelector ?? (s => s);
			return this.Dispatch<PingDescriptor, PingRequestParameters, PingResponse>(
				pingSelector,
				(p, d) => this.RawDispatch.PingDispatch<PingResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(Func<PingDescriptor, PingDescriptor> pingSelector = null)
		{
			pingSelector = pingSelector ?? (s => s);
			return this.DispatchAsync<PingDescriptor, PingRequestParameters, PingResponse, IPingResponse>(
				pingSelector,
				(p, d) => this.RawDispatch.PingDispatchAsync<PingResponse>(p)
			);
		}

		/// <inheritdoc />
		public IPingResponse Ping(IPingRequest pingRequest)
		{
			return this.Dispatch<IPingRequest, PingRequestParameters, PingResponse>(
				pingRequest,
				(p, d) => this.RawDispatch.PingDispatch<PingResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IPingResponse> PingAsync(IPingRequest pingRequest)
		{
			return this.DispatchAsync<IPingRequest, PingRequestParameters, PingResponse, IPingResponse>(
				pingRequest,
				(p, d) => this.RawDispatch.PingDispatchAsync<PingResponse>(p)
			);
		}
	}
}
