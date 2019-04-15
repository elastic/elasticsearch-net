using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Delete an index alias
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#deleting
		/// </summary>
		/// <param name="request">A descriptor that describes the delete alias request</param>
		DeleteAliasResponse DeleteAlias(IDeleteAliasRequest request);

		/// <inheritdoc />
		Task<DeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		DeleteAliasResponse DeleteAlias(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null);

		/// <inheritdoc />
		Task<DeleteAliasResponse> DeleteAliasAsync(
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken cancellationToken = default
		);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteAliasResponse DeleteAlias(IDeleteAliasRequest request) =>
			DoRequest<IDeleteAliasRequest, DeleteAliasResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public DeleteAliasResponse DeleteAlias(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null) =>
			DeleteAlias(selector.InvokeOrDefault(new DeleteAliasDescriptor(indices, names)));

		/// <inheritdoc />
		public Task<DeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteAliasRequest, DeleteAliasResponse, DeleteAliasResponse>(request, request.RequestParameters, ct);

		/// <inheritdoc />
		public Task<DeleteAliasResponse> DeleteAliasAsync(
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken cancellationToken = default
		) => DeleteAliasAsync(selector.InvokeOrDefault(new DeleteAliasDescriptor(indices, names)), cancellationToken);
	}
}
