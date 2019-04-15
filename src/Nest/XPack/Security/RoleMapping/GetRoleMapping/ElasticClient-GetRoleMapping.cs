using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetRoleMappingResponse GetRoleMapping(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null);

		/// <inheritdoc />
		GetRoleMappingResponse GetRoleMapping(IGetRoleMappingRequest request);

		/// <inheritdoc />
		Task<GetRoleMappingResponse> GetRoleMappingAsync(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetRoleMappingResponse> GetRoleMappingAsync(IGetRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetRoleMappingResponse GetRoleMapping(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null) =>
			GetRoleMapping(selector.InvokeOrDefault(new GetRoleMappingDescriptor()));

		/// <inheritdoc />
		public GetRoleMappingResponse GetRoleMapping(IGetRoleMappingRequest request) =>
			DoRequest<IGetRoleMappingRequest, GetRoleMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetRoleMappingResponse> GetRoleMappingAsync(
			Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null,
			CancellationToken ct = default
		) => GetRoleMappingAsync(selector.InvokeOrDefault(new GetRoleMappingDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetRoleMappingResponse> GetRoleMappingAsync(IGetRoleMappingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetRoleMappingRequest, GetRoleMappingResponse, GetRoleMappingResponse>(request, request.RequestParameters, ct);
	}
}
