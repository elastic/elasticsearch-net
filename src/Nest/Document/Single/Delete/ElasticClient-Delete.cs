using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The delete API allows to delete a typed JSON document from a specific index based on its id.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html</a>
		/// </summary>
		/// <typeparam name="TDocument">The type used to infer the default index and typename</typeparam>
		/// <param name="selector">Describe the delete operation, i.e type/index/id</param>
		DeleteResponse Delete<TDocument>(DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null) where TDocument : class;

		/// <inheritdoc />
		DeleteResponse Delete(IDeleteRequest request);

		/// <inheritdoc />
		Task<DeleteResponse> DeleteAsync<TDocument>(
			DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null,
			CancellationToken ct = default
		) where TDocument : class;

		/// <inheritdoc />
		Task<DeleteResponse> DeleteAsync(IDeleteRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteResponse Delete<TDocument>(DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null) where TDocument : class =>
			Delete(selector.InvokeOrDefault(new DeleteDescriptor<TDocument>(document.Self.Index, document.Self.Id)));

		/// <inheritdoc />
		public DeleteResponse Delete(IDeleteRequest request) =>
			DoRequest<IDeleteRequest, DeleteResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteResponse> DeleteAsync<TDocument>(
			DocumentPath<TDocument> document,
			Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null,
			CancellationToken ct = default
		)
			where TDocument : class =>
			DeleteAsync(selector.InvokeOrDefault(new DeleteDescriptor<TDocument>(document.Self.Index, document.Self.Id)), ct);

		/// <inheritdoc />
		public Task<DeleteResponse> DeleteAsync(IDeleteRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteRequest, DeleteResponse>(request, request.RequestParameters, ct);
	}
}
