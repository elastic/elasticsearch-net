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
		Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc />
		IDeleteAliasResponse DeleteAlias(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null);

		/// <inheritdoc />
		Task<IDeleteAliasResponse> DeleteAliasAsync(
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest request) =>
			Dispatcher.Dispatch<IDeleteAliasRequest, DeleteAliasRequestParameters, DeleteAliasResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesDeleteAliasDispatch<DeleteAliasResponse>(p)
			);

		/// <inheritdoc />
		public IDeleteAliasResponse DeleteAlias(Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null) =>
			DeleteAlias(selector.InvokeOrDefault(new DeleteAliasDescriptor(indices, names)));

		/// <inheritdoc />
		public Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IDeleteAliasRequest, DeleteAliasRequestParameters, DeleteAliasResponse, IDeleteAliasResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesDeleteAliasDispatchAsync<DeleteAliasResponse>(p, c)
			);

		/// <inheritdoc />
		public Task<IDeleteAliasResponse> DeleteAliasAsync(
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => DeleteAliasAsync(selector.InvokeOrDefault(new DeleteAliasDescriptor(indices, names)), cancellationToken);
	}
}
