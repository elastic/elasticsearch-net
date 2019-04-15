using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		DeleteRoleMappingResponse DeleteRoleMapping(Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null);

		/// <inheritdoc />
		DeleteRoleMappingResponse DeleteRoleMapping(IDeleteRoleMappingRequest request);

		/// <inheritdoc />
		Task<DeleteRoleMappingResponse> DeleteRoleMappingAsync(Name role,
			Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteRoleMappingResponse> DeleteRoleMappingAsync(IDeleteRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteRoleMappingResponse
			DeleteRoleMapping(Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null) =>
			DeleteRoleMapping(selector.InvokeOrDefault(new DeleteRoleMappingDescriptor(role)));

		/// <inheritdoc />
		public DeleteRoleMappingResponse DeleteRoleMapping(IDeleteRoleMappingRequest request) =>
			DoRequest<IDeleteRoleMappingRequest, DeleteRoleMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteRoleMappingResponse> DeleteRoleMappingAsync(
			Name role,
			Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null,
			CancellationToken ct = default
		) => DeleteRoleMappingAsync(selector.InvokeOrDefault(new DeleteRoleMappingDescriptor(role)), ct);

		/// <inheritdoc />
		public Task<DeleteRoleMappingResponse> DeleteRoleMappingAsync(IDeleteRoleMappingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteRoleMappingRequest, DeleteRoleMappingResponse>(request, request.RequestParameters, ct);
	}
}
