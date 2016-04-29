using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The multi percolate API allows to bundle multiple percolate requests into a single request,
		/// similar to what the multi search API does to search requests.
		/// </summary>
		/// <param name="selector">A descriptor to describe the multi percolate operation</param>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
		IMultiPercolateResponse MultiPercolate(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
		IMultiPercolateResponse MultiPercolate(IMultiPercolateRequest request);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
		Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
		Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest request);
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
		public IMultiPercolateResponse MultiPercolate(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector) =>
			this.MultiPercolate(selector?.Invoke(new MultiPercolateDescriptor()));

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
		public IMultiPercolateResponse MultiPercolate(IMultiPercolateRequest request) =>
			this.Dispatcher.Dispatch<IMultiPercolateRequest, MultiPercolateRequestParameters, MultiPercolateResponse>(
				request, this.LowLevelDispatch.MpercolateDispatch<MultiPercolateResponse>
			);

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
		public Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector) =>
			this.MultiPercolateAsync(selector?.Invoke(new MultiPercolateDescriptor()));

		/// <inheritdoc/>
		[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
		public Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest request) =>
			this.Dispatcher.DispatchAsync<IMultiPercolateRequest, MultiPercolateRequestParameters, MultiPercolateResponse, IMultiPercolateResponse>(
				request, this.LowLevelDispatch.MpercolateDispatchAsync<MultiPercolateResponse>
			);
	}
}
