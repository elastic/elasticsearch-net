using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The delete index API allows to delete an existing index.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-index.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the delete index operation</param>
		IIndicesResponse DeleteIndex(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null);

		/// <inheritdoc/>
		IIndicesResponse DeleteIndex(IDeleteIndexRequest request);

		/// <inheritdoc/>
		Task<IIndicesResponse> DeleteIndexAsync(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null);

		/// <inheritdoc/>
		Task<IIndicesResponse> DeleteIndexAsync(IDeleteIndexRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IIndicesResponse DeleteIndex(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null) =>
			this.DeleteIndex(selector.InvokeOrDefault(new DeleteIndexDescriptor(indices)));

		/// <inheritdoc/>
		public IIndicesResponse DeleteIndex(IDeleteIndexRequest request) => 
			this.Dispatcher.Dispatch<IDeleteIndexRequest, DeleteIndexRequestParameters, IndicesResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteDispatch<IndicesResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IIndicesResponse> DeleteIndexAsync(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null) =>
			this.DeleteIndexAsync(selector.InvokeOrDefault(new DeleteIndexDescriptor(indices)));

		/// <inheritdoc/>
		public Task<IIndicesResponse> DeleteIndexAsync(IDeleteIndexRequest request) => 
			this.Dispatcher.DispatchAsync<IDeleteIndexRequest, DeleteIndexRequestParameters, IndicesResponse, IIndicesResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesDeleteDispatchAsync<IndicesResponse>(p)
			);
	}
}