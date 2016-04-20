using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteRoleResponse DeleteRole(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null);

		/// <inheritdoc/>
		IDeleteRoleResponse DeleteRole(IDeleteRoleRequest request);

		/// <inheritdoc/>
		Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request);
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
				(p, d) =>this.LowLevelDispatch.ShieldDeleteRoleDispatch<DeleteRoleResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteRoleResponse> DeleteRoleAsync(Name role, Func<DeleteRoleDescriptor, IDeleteRoleRequest> selector = null) =>
			this.DeleteRoleAsync(selector.InvokeOrDefault(new DeleteRoleDescriptor(role)));

		/// <inheritdoc/>
		public Task<IDeleteRoleResponse> DeleteRoleAsync(IDeleteRoleRequest request) =>
			this.Dispatcher.DispatchAsync<IDeleteRoleRequest, DeleteRoleRequestParameters, DeleteRoleResponse, IDeleteRoleResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.ShieldDeleteRoleDispatchAsync<DeleteRoleResponse>(p)
			);
	}
}
