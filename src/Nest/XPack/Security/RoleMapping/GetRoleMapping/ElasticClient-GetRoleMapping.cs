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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetRoleMappingResponse> GetRoleMappingAsync(IGetRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetRoleMappingResponse GetRoleMapping(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null) =>
			GetRoleMapping(selector.InvokeOrDefault(new GetRoleMappingDescriptor()));

		/// <inheritdoc />
		public IGetRoleMappingResponse GetRoleMapping(IGetRoleMappingRequest request) =>
			Dispatch2<IGetRoleMappingRequest, GetRoleMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetRoleMappingResponse> GetRoleMappingAsync(
			Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null,
			CancellationToken ct = default
		) => GetRoleMappingAsync(selector.InvokeOrDefault(new GetRoleMappingDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetRoleMappingResponse> GetRoleMappingAsync(IGetRoleMappingRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetRoleMappingRequest, IGetRoleMappingResponse, GetRoleMappingResponse>(request, request.RequestParameters, ct);
	}
}
