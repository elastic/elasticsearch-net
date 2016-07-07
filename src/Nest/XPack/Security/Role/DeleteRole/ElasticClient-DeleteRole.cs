using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteRoleResponse DeleteRole(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null);

		/// <inheritdoc/>
		IDeleteRoleResponse DeleteRole(IDeleteRoleRequest request);

		/// <inheritdoc/>
		Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteRoleResponse DeleteRole(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null) =>
			this.DeleteRole(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)));

		/// <inheritdoc/>
		public IDeleteRoleResponse DeleteRole(IDeleteRoleRequest request) =>
			this.Dispatcher.Dispatch<IDeleteRoleRequest, DeleteRoleRequestParameters, DeleteRoleResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackSecurityDeleteRoleDispatch<DeleteRoleResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteRoleAsync(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteRoleRequest, DeleteRoleRequestParameters, DeleteRoleResponse, IDeleteRoleResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityDeleteRoleDispatchAsync<DeleteRoleResponse>(p, c)
			);
	}
}
