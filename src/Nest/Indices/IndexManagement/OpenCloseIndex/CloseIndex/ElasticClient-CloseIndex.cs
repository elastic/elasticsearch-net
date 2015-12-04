using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it. 
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked 
		/// for read/write operations. 
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html</a>
		/// </summary>
		/// <param name="selector">A descriptor thata describes the close index operation</param>
		IIndicesOperationResponse CloseIndex(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse CloseIndex(ICloseIndexRequest closeIndexRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CloseIndexAsync(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CloseIndexAsync(ICloseIndexRequest closeIndexRequest);
	}

	public partial class ElasticClient
	{

		/// <inheritdoc/>
		public IIndicesOperationResponse CloseIndex(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null) =>
			this.CloseIndex(selector.InvokeOrDefault(new CloseIndexDescriptor(indices)));

		/// <inheritdoc/>
		public IIndicesOperationResponse CloseIndex(ICloseIndexRequest closeIndexRequest) => 
			this.Dispatcher.Dispatch<ICloseIndexRequest, CloseIndexRequestParameters, IndicesOperationResponse>(
				closeIndexRequest,
				(p, d) => this.LowLevelDispatch.IndicesCloseDispatch<IndicesOperationResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> CloseIndexAsync(Indices indices, Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null) =>
			this.CloseIndexAsync(selector.InvokeOrDefault(new CloseIndexDescriptor(indices)));

		/// <inheritdoc/>
		public Task<IIndicesOperationResponse> CloseIndexAsync(ICloseIndexRequest closeIndexRequest) => 
			this.Dispatcher.DispatchAsync<ICloseIndexRequest, CloseIndexRequestParameters, IndicesOperationResponse, IIndicesOperationResponse>(
				closeIndexRequest,
				(p, d) => this.LowLevelDispatch.IndicesCloseDispatchAsync<IndicesOperationResponse>(p)
			);
	}
}