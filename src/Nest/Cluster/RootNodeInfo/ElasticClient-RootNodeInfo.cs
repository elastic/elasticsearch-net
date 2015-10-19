using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		IRootNodeInfoResponse RootNodeInfo(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null);

		/// <inheritdoc/>
		IRootNodeInfoResponse RootNodeInfo(IRootNodeInfoRequest infoRequest);

		/// <inheritdoc/>
		Task<IRootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null);

		/// <inheritdoc/>
		Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest infoRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRootNodeInfoResponse RootNodeInfo(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null) =>
			this.RootNodeInfo(selector.InvokeOrDefault(new RootNodeInfoDescriptor()));

		/// <inheritdoc/>
		public IRootNodeInfoResponse RootNodeInfo(IRootNodeInfoRequest infoRequest) => 
			this.Dispatcher.Dispatch<IRootNodeInfoRequest, RootNodeInfoRequestParameters, RootNodeInfoResponse>(
				infoRequest,
				(p, d) => this.LowLevelDispatch.InfoDispatch<RootNodeInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IRootNodeInfoResponse> RootNodeInfoAsync(Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null) =>
			this.RootNodeInfoAsync(selector.InvokeOrDefault(new RootNodeInfoDescriptor()));

		/// <inheritdoc/>
		public Task<IRootNodeInfoResponse> RootNodeInfoAsync(IRootNodeInfoRequest infoRequest) => 
			this.Dispatcher.DispatchAsync<IRootNodeInfoRequest, RootNodeInfoRequestParameters, RootNodeInfoResponse, IRootNodeInfoResponse>(
				infoRequest,
				(p, d) => this.LowLevelDispatch.InfoDispatchAsync<RootNodeInfoResponse>(p)
			);
	}
}