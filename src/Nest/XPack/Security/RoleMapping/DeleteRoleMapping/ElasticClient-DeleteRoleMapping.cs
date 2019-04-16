using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDeleteRoleMappingResponse DeleteRoleMapping(Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null);

		/// <inheritdoc />
		IDeleteRoleMappingResponse DeleteRoleMapping(IDeleteRoleMappingRequest request);

		/// <inheritdoc />
		Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(Name role,
			Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(IDeleteRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteRoleMappingResponse
			DeleteRoleMapping(Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null) =>
			DeleteRoleMapping(selector.InvokeOrDefault(new DeleteRoleMappingDescriptor(role)));

		/// <inheritdoc />
		public IDeleteRoleMappingResponse DeleteRoleMapping(IDeleteRoleMappingRequest request) =>
			DoRequest<IDeleteRoleMappingRequest, DeleteRoleMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(
			Name role,
			Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null,
			CancellationToken ct = default
		) => DeleteRoleMappingAsync(selector.InvokeOrDefault(new DeleteRoleMappingDescriptor(role)), ct);

		/// <inheritdoc />
		public Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(IDeleteRoleMappingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteRoleMappingRequest, IDeleteRoleMappingResponse, DeleteRoleMappingResponse>(request, request.RequestParameters, ct);
	}
}
