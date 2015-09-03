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
		IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, IInfoRequest> selector = null);

		/// <inheritdoc/>
		IRootInfoResponse RootNodeInfo(IInfoRequest infoRequest);

		/// <inheritdoc/>
		Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, IInfoRequest> selector = null);

		/// <inheritdoc/>
		Task<IRootInfoResponse> RootNodeInfoAsync(IInfoRequest infoRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, IInfoRequest> selector = null) =>
			this.RootNodeInfo(selector.InvokeOrDefault(new InfoDescriptor()));

		/// <inheritdoc/>
		public IRootInfoResponse RootNodeInfo(IInfoRequest infoRequest) => 
			this.Dispatcher.Dispatch<IInfoRequest, InfoRequestParameters, RootInfoResponse>(
				infoRequest,
				(p, d) => this.LowLevelDispatch.InfoDispatch<RootInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, IInfoRequest> selector = null) =>
			this.RootNodeInfoAsync(selector.InvokeOrDefault(new InfoDescriptor()));

		/// <inheritdoc/>
		public Task<IRootInfoResponse> RootNodeInfoAsync(IInfoRequest infoRequest) => 
			this.Dispatcher.DispatchAsync<IInfoRequest, InfoRequestParameters, RootInfoResponse, IRootInfoResponse>(
				infoRequest,
				(p, d) => this.LowLevelDispatch.InfoDispatchAsync<RootInfoResponse>(p)
			);
	}
}