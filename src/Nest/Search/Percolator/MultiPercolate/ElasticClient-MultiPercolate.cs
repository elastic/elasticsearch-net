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
		/// <para> </para><a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/search-percolate.html#_multi_percolate_api">https://www.elastic.co/guide/en/elasticsearch/reference/current/search-percolate.html#_multi_percolate_api</a>
		/// </summary>
		/// <param name="selector">A descriptor to describe the multi percolate operation</param>
		IMultiPercolateResponse MultiPercolate(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector);

		/// <inheritdoc/>
		IMultiPercolateResponse MultiPercolate(IMultiPercolateRequest request);

		/// <inheritdoc/>
		Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector);

		/// <inheritdoc/>
		Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest request);
	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMultiPercolateResponse MultiPercolate(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector) =>
			this.MultiPercolate(selector?.Invoke(new MultiPercolateDescriptor()));

		/// <inheritdoc/>
		public IMultiPercolateResponse MultiPercolate(IMultiPercolateRequest request) => 
			this.Dispatcher.Dispatch<IMultiPercolateRequest, MultiPercolateRequestParameters, MultiPercolateResponse>(
				request, this.LowLevelDispatch.MpercolateDispatch<MultiPercolateResponse>
			);

		/// <inheritdoc/>
		public Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, IMultiPercolateRequest> selector) => 
			this.MultiPercolateAsync(selector?.Invoke(new MultiPercolateDescriptor()));

		/// <inheritdoc/>
		public Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest request) => 
			this.Dispatcher.DispatchAsync<IMultiPercolateRequest, MultiPercolateRequestParameters, MultiPercolateResponse, IMultiPercolateResponse>(
				request, this.LowLevelDispatch.MpercolateDispatchAsync<MultiPercolateResponse>
			);
	}
}