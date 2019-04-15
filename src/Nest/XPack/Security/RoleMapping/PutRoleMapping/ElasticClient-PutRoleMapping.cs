using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		PutRoleMappingResponse PutRoleMapping(Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null);

		/// <inheritdoc />
		PutRoleMappingResponse PutRoleMapping(IPutRoleMappingRequest request);

		/// <inheritdoc />
		Task<PutRoleMappingResponse> PutRoleMappingAsync(Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<PutRoleMappingResponse> PutRoleMappingAsync(IPutRoleMappingRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutRoleMappingResponse PutRoleMapping(Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null) =>
			PutRoleMapping(selector.InvokeOrDefault(new PutRoleMappingDescriptor(role)));

		/// <inheritdoc />
		public PutRoleMappingResponse PutRoleMapping(IPutRoleMappingRequest request) =>
			DoRequest<IPutRoleMappingRequest, PutRoleMappingResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PutRoleMappingResponse> PutRoleMappingAsync(
			Name role,
			Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null,
			CancellationToken cancellationToken = default
		) => PutRoleMappingAsync(selector.InvokeOrDefault(new PutRoleMappingDescriptor(role)), cancellationToken);

		/// <inheritdoc />
		public Task<PutRoleMappingResponse> PutRoleMappingAsync(IPutRoleMappingRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutRoleMappingRequest, PutRoleMappingResponse, PutRoleMappingResponse>(request, request.RequestParameters, ct);
	}
}
