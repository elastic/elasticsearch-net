using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDeleteRoleResponse DeleteRole(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null);

		/// <inheritdoc />
		IDeleteRoleResponse DeleteRole(IDeleteRoleRequest request);

		/// <inheritdoc />
		Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteRoleResponse DeleteRole(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null) =>
			DeleteRole(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)));

		/// <inheritdoc />
		public IDeleteRoleResponse DeleteRole(IDeleteRoleRequest request) =>
			Dispatch2<IDeleteRoleRequest, DeleteRoleResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteRoleResponse> DeleteRoleAsync(
			Name role,
			Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null,
			CancellationToken ct = default
		) => DeleteRoleAsync(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)), ct);

		/// <inheritdoc />
		public Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteRoleRequest, IDeleteRoleResponse, DeleteRoleResponse>(request, request.RequestParameters, ct);
	}
}
