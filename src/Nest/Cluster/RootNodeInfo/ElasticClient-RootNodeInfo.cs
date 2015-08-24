using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? (i => i);
			return this.Dispatcher.Dispatch<InfoDescriptor, InfoRequestParameters, RootInfoResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.InfoDispatch<RootInfoResponse>(p)
			);
		}

		/// <inheritdoc/>
		public IRootInfoResponse RootNodeInfo(IInfoRequest infoRequest)
		{
			return this.Dispatcher.Dispatch<IInfoRequest, InfoRequestParameters, RootInfoResponse>(
				infoRequest,
				(p, d) => this.LowLevelDispatch.InfoDispatch<RootInfoResponse>(p)
				);
		}

		/// <inheritdoc/>
		public Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null)
		{
			selector = selector ?? (i => i);
			return this.Dispatcher.DispatchAsync<InfoDescriptor, InfoRequestParameters, RootInfoResponse, IRootInfoResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.InfoDispatchAsync<RootInfoResponse>(p)
			);
		}

		/// <inheritdoc/>
		public Task<IRootInfoResponse> RootNodeInfoAsync(IInfoRequest inforRequest)
		{
			return this.Dispatcher.DispatchAsync<IInfoRequest, InfoRequestParameters, RootInfoResponse, IRootInfoResponse>(
				inforRequest,
				(p, d) => this.LowLevelDispatch.InfoDispatchAsync<RootInfoResponse>(p)
			);
		}

	}
}