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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteRoleResponse DeleteRole(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null) =>
			DeleteRole(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)));

		/// <inheritdoc />
		public IDeleteRoleResponse DeleteRole(IDeleteRoleRequest request) =>
			Dispatcher.Dispatch<IDeleteRoleRequest, DeleteRoleRequestParameters, DeleteRoleResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityDeleteRoleDispatch<DeleteRoleResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteRoleAsync(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IDeleteRoleRequest, DeleteRoleRequestParameters, DeleteRoleResponse, IDeleteRoleResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SecurityDeleteRoleDispatchAsync<DeleteRoleResponse>(p, c)
			);
	}
}
