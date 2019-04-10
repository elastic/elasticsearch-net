using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPutRoleMappingResponse PutRoleMapping(Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null);

		/// <inheritdoc />
		IPutRoleMappingResponse PutRoleMapping(IPutRoleMappingRequest request);

		/// <inheritdoc />
		Task<IPutRoleMappingResponse> PutRoleMappingAsync(Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IPutRoleMappingResponse> PutRoleMappingAsync(IPutRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutRoleMappingResponse PutRoleMapping(Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null) =>
			PutRoleMapping(selector.InvokeOrDefault(new PutRoleMappingDescriptor(role)));

		/// <inheritdoc />
		public IPutRoleMappingResponse PutRoleMapping(IPutRoleMappingRequest request) =>
			DoRequest<IPutRoleMappingRequest, PutRoleMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPutRoleMappingResponse> PutRoleMappingAsync(
			Name role,
			Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null,
			CancellationToken cancellationToken = default
		) => PutRoleMappingAsync(selector.InvokeOrDefault(new PutRoleMappingDescriptor(role)), cancellationToken);

		/// <inheritdoc />
		public Task<IPutRoleMappingResponse> PutRoleMappingAsync(IPutRoleMappingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutRoleMappingRequest, IPutRoleMappingResponse, PutRoleMappingResponse>(request, request.RequestParameters, ct);
	}
}
