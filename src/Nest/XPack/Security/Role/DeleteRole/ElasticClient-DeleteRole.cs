using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		DeleteRoleResponse DeleteRole(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null);

		/// <inheritdoc />
		DeleteRoleResponse DeleteRole(IDeleteRoleRequest request);

		/// <inheritdoc />
		Task<DeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteRoleResponse DeleteRole(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null) =>
			DeleteRole(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)));

		/// <inheritdoc />
		public DeleteRoleResponse DeleteRole(IDeleteRoleRequest request) =>
			DoRequest<IDeleteRoleRequest, DeleteRoleResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteRoleResponse> DeleteRoleAsync(
			Name role,
			Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null,
			CancellationToken ct = default
		) => DeleteRoleAsync(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)), ct);

		/// <inheritdoc />
		public Task<DeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteRoleRequest, DeleteRoleResponse>(request, request.RequestParameters, ct);
	}
}
