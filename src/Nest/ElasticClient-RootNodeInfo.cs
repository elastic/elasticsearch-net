using System;
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
			return this.Dispatch<InfoDescriptor, InfoRequestParameters, RootInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.InfoDispatch<RootInfoResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? (i => i);
			return this.DispatchAsync<InfoDescriptor, InfoRequestParameters, RootInfoResponse, IRootInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.InfoDispatchAsync<RootInfoResponse>(p)
			);
		}
	}
}