using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteRoleMappingResponse DeleteRoleMapping(Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null);

		/// <inheritdoc/>
		IDeleteRoleMappingResponse DeleteRoleMapping(IDeleteRoleMappingRequest request);

		/// <inheritdoc/>
		Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(IDeleteRoleMappingRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteRoleMappingResponse DeleteRoleMapping(Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null) =>
			this.DeleteRoleMapping(selector.InvokeOrDefault(new DeleteRoleMappingDescriptor(role)));

		/// <inheritdoc/>
		public IDeleteRoleMappingResponse DeleteRoleMapping(IDeleteRoleMappingRequest request) =>
			this.Dispatcher.Dispatch<IDeleteRoleMappingRequest, DeleteRoleMappingRequestParameters, DeleteRoleMappingResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackSecurityDeleteRoleMappingDispatch<DeleteRoleMappingResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(Name role, Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteRoleMappingAsync(selector.InvokeOrDefault(new DeleteRoleMappingDescriptor(role)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(IDeleteRoleMappingRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteRoleMappingRequest, DeleteRoleMappingRequestParameters, DeleteRoleMappingResponse, IDeleteRoleMappingResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityDeleteRoleMappingDispatchAsync<DeleteRoleMappingResponse>(p, c)
			);
	}
}
