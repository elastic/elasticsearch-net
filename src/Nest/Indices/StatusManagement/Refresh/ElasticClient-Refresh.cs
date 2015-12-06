using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The refresh API allows to explicitly refresh one or more index, making all operations performed since the last refresh 
		/// available for search. The (near) real-time capabilities depend on the index engine used. 
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-refresh.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-refresh.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the refresh operation</param>
		IShardsOperationResponse Refresh(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector = null);

		/// <inheritdoc/>
		IShardsOperationResponse Refresh(IRefreshRequest request);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> RefreshAsync(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector = null);

		/// <inheritdoc/>
		Task<IShardsOperationResponse> RefreshAsync(IRefreshRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IShardsOperationResponse Refresh(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector = null) =>
			this.Refresh(selector.InvokeOrDefault(new RefreshDescriptor().Index(indices)));

		/// <inheritdoc/>
		public IShardsOperationResponse Refresh(IRefreshRequest request) => 
			this.Dispatcher.Dispatch<IRefreshRequest, RefreshRequestParameters, ShardsOperationResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesRefreshDispatch<ShardsOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> RefreshAsync(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector = null) => 
			this.RefreshAsync(selector.InvokeOrDefault(new RefreshDescriptor().Index(indices)));

		/// <inheritdoc/>
		public Task<IShardsOperationResponse> RefreshAsync(IRefreshRequest request) => 
			this.Dispatcher.DispatchAsync<IRefreshRequest, RefreshRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesRefreshDispatchAsync<ShardsOperationResponse>(p)
			);
	}
}