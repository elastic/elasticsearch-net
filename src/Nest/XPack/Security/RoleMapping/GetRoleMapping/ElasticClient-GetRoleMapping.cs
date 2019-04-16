using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetRoleMappingResponse GetRoleMapping(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null);

		/// <inheritdoc />
		IGetRoleMappingResponse GetRoleMapping(IGetRoleMappingRequest request);

		/// <inheritdoc />
		Task<IGetRoleMappingResponse> GetRoleMappingAsync(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetRoleMappingResponse> GetRoleMappingAsync(IGetRoleMappingRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetRoleMappingResponse GetRoleMapping(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null) =>
			GetRoleMapping(selector.InvokeOrDefault(new GetRoleMappingDescriptor()));

		/// <inheritdoc />
		public IGetRoleMappingResponse GetRoleMapping(IGetRoleMappingRequest request) =>
			Dispatcher.Dispatch<IGetRoleMappingRequest, GetRoleMappingRequestParameters, GetRoleMappingResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityGetRoleMappingDispatch<GetRoleMappingResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetRoleMappingResponse> GetRoleMappingAsync(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetRoleMappingAsync(selector.InvokeOrDefault(new GetRoleMappingDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetRoleMappingResponse> GetRoleMappingAsync(IGetRoleMappingRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetRoleMappingRequest, GetRoleMappingRequestParameters, GetRoleMappingResponse, IGetRoleMappingResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SecurityGetRoleMappingDispatchAsync<GetRoleMappingResponse>(p, c)
			);
	}
}
