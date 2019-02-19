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
		IDeleteResponse Delete<TDocument>(DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null) where TDocument : class;

		/// <inheritdoc />
		IDeleteResponse Delete(IDeleteRequest request);

		/// <inheritdoc />
		Task<IDeleteResponse> DeleteAsync<TDocument>(
			DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class;

		/// <inheritdoc />
		Task<IDeleteResponse> DeleteAsync(IDeleteRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteResponse Delete<TDocument>(DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null) where TDocument : class =>
			Delete(selector.InvokeOrDefault(new DeleteDescriptor<TDocument>(document.Self.Index, document.Self.Id)));

		/// <inheritdoc />
		public IDeleteResponse Delete(IDeleteRequest request) =>
			Dispatcher.Dispatch<IDeleteRequest, DeleteRequestParameters, DeleteResponse>(
				request,
				(p, d) => LowLevelDispatch.DeleteDispatch<DeleteResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteResponse> DeleteAsync<TDocument>(
			DocumentPath<TDocument> document, Func<DeleteDescriptor<TDocument>, IDeleteRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) where TDocument : class => DeleteAsync(selector.InvokeOrDefault(new DeleteDescriptor<TDocument>(document.Self.Index, document.Self.Id)),
			cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteResponse> DeleteAsync(IDeleteRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IDeleteRequest, DeleteRequestParameters, DeleteResponse, IDeleteResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.DeleteDispatchAsync<DeleteResponse>(p, c)
			);
	}
}
