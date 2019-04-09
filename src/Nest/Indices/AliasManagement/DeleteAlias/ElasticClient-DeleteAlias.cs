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
		IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest request);

		/// <inheritdoc />
		Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		IDeleteAliasResponse DeleteAlias(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null);

		/// <inheritdoc />
		Task<IDeleteAliasResponse> DeleteAliasAsync(
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken cancellationToken = default
		);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest request) =>
			Dispatch2<IDeleteAliasRequest, DeleteAliasResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public IDeleteAliasResponse DeleteAlias(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null) =>
			DeleteAlias(selector.InvokeOrDefault(new DeleteAliasDescriptor(indices, names)));

		/// <inheritdoc />
		public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteAliasRequest, IDeleteAliasResponse, DeleteAliasResponse>(request, request.RequestParameters, ct);

		/// <inheritdoc />
		public Task<IDeleteAliasResponse> DeleteAliasAsync(
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken cancellationToken = default
		) => DeleteAliasAsync(selector.InvokeOrDefault(new DeleteAliasDescriptor(indices, names)), cancellationToken);
	}
}
