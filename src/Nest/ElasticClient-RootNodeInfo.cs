using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? (i => i);
			return this.Dispatch<InfoDescriptor, InfoQueryString, RootInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.InfoDispatch<RootInfoResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? (i => i);
			return this.DispatchAsync<InfoDescriptor, InfoQueryString, RootInfoResponse, IRootInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.InfoDispatchAsync<RootInfoResponse>(p)
			);
		}
	}
}