using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IShardsOperationResponse Refresh(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector = null)
		{
			refreshSelector = refreshSelector ?? (s => s);
			return this.Dispatch<RefreshDescriptor, RefreshRequestParameters, ShardsOperationResponse>(
				refreshSelector,
				(p, d) => this.RawDispatch.IndicesRefreshDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public IShardsOperationResponse Refresh(IRefreshRequest refreshRequest)
		{
			return this.Dispatch<IRefreshRequest, RefreshRequestParameters, ShardsOperationResponse>(
				refreshRequest,
				(p, d) => this.RawDispatch.IndicesRefreshDispatch<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> RefreshAsync(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector = null)
		{
			refreshSelector = refreshSelector ?? (s => s);
			return this.DispatchAsync<RefreshDescriptor, RefreshRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				refreshSelector,
				(p, d) => this.RawDispatch.IndicesRefreshDispatchAsync<ShardsOperationResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IShardsOperationResponse> RefreshAsync(IRefreshRequest refreshRequest)
		{
			return this.DispatchAsync<IRefreshRequest, RefreshRequestParameters, ShardsOperationResponse, IShardsOperationResponse>(
				refreshRequest,
				(p, d) => this.RawDispatch.IndicesRefreshDispatchAsync<ShardsOperationResponse>(p)
			);
		}

	}
}