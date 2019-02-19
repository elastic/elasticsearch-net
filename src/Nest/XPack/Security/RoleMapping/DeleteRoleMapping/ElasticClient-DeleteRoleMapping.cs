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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(IDeleteRoleMappingRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
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
			Dispatcher.Dispatch<IDeleteRoleMappingRequest, DeleteRoleMappingRequestParameters, DeleteRoleMappingResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityDeleteRoleMappingDispatch<DeleteRoleMappingResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(Name role,
			Func<DeleteRoleMappingDescriptor, IDeleteRoleMappingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteRoleMappingAsync(selector.InvokeOrDefault(new DeleteRoleMappingDescriptor(role)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteRoleMappingResponse> DeleteRoleMappingAsync(IDeleteRoleMappingRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IDeleteRoleMappingRequest, DeleteRoleMappingRequestParameters, DeleteRoleMappingResponse, IDeleteRoleMappingResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.SecurityDeleteRoleMappingDispatchAsync<DeleteRoleMappingResponse>(p, c)
				);
	}
}
