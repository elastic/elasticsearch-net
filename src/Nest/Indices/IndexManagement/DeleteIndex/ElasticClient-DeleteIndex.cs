using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The delete index API allows to delete an existing index.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-index.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the delete index operation</param>
		IDeleteIndexResponse DeleteIndex(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null);

		/// <inheritdoc />
		IDeleteIndexResponse DeleteIndex(IDeleteIndexRequest request);

		/// <inheritdoc />
		Task<IDeleteIndexResponse> DeleteIndexAsync(
			Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteIndexResponse> DeleteIndexAsync(IDeleteIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteIndexResponse DeleteIndex(Indices indices, Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null) =>
			DeleteIndex(selector.InvokeOrDefault(new DeleteIndexDescriptor(indices)));

		/// <inheritdoc />
		public IDeleteIndexResponse DeleteIndex(IDeleteIndexRequest request) =>
			DoRequest<IDeleteIndexRequest, DeleteIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteIndexResponse> DeleteIndexAsync(
			Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null,
			CancellationToken ct = default
		) => DeleteIndexAsync(selector.InvokeOrDefault(new DeleteIndexDescriptor(indices)), ct);

		/// <inheritdoc />
		public Task<IDeleteIndexResponse> DeleteIndexAsync(IDeleteIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteIndexRequest, IDeleteIndexResponse, DeleteIndexResponse>(request, request.RequestParameters, ct);
	}
}
