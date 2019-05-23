using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The update API allows to update a document based on a script provided.
		/// <para>
		/// The operation gets the document (collocated with the shard) from the index, runs the script
		/// (with optional script language and parameters), and index back the result
		/// (also allows to delete, or ignore the operation).
		/// </para>
		/// <para>It uses versioning to make sure no updates have happened during the "get" and "reindex".</para>
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-update.html
		/// </summary>
		/// <typeparam name="TDocument">The type to describe the document to be updated</typeparam>
		/// <param name="selector">a descriptor that describes the update operation</param>
		UpdateResponse<TDocument> Update<TDocument>(DocumentPath<TDocument> documentPath,
			Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest> selector
		) where TDocument : class;

		/// <inheritdoc />
		UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IUpdateRequest request)
			where TDocument : class
			where TPartialDocument : class;

		/// <inheritdoc />
		Task<UpdateResponse<TDocument>> UpdateAsync<TDocument>(
			DocumentPath<TDocument> documentPath,
			Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest> selector,
			CancellationToken cancellationToken = default
		) where TDocument : class;

		/// <inheritdoc />
		Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(
			IUpdateRequest request,
			CancellationToken ct = default
		)
			where TDocument : class
			where TPartialDocument : class;
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public UpdateResponse<TDocument> Update<TDocument>(DocumentPath<TDocument> documentPath,
			Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest> selector
		) where TDocument : class =>
			Update<TDocument, TDocument>(documentPath, selector);

		/// <inheritdoc />
		public UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IUpdateRequest request)
			where TDocument : class
			where TPartialDocument : class =>
			DoRequest<IUpdateRequest<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<UpdateResponse<TDocument>> UpdateAsync<TDocument>(
			DocumentPath<TDocument> documentPath,
			Func<UpdateDescriptor<TDocument, TDocument>, IUpdateRequest> selector,
			CancellationToken cancellationToken = default
		)
			where TDocument : class =>
			UpdateAsync<TDocument, TDocument>(documentPath, selector, cancellationToken);

		/// <inheritdoc />
		public Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(
			IUpdateRequest request,
			CancellationToken ct = default
		)
			where TDocument : class
			where TPartialDocument : class =>
			DoRequestAsync<IUpdateRequest, UpdateResponse<TDocument>>(request, request.RequestParameters, ct);
	}
}
