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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IPutRoleMappingResponse> PutRoleMappingAsync(IPutRoleMappingRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutRoleMappingResponse PutRoleMapping(Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null) =>
			PutRoleMapping(selector.InvokeOrDefault(new PutRoleMappingDescriptor(role)));

		/// <inheritdoc />
		public IPutRoleMappingResponse PutRoleMapping(IPutRoleMappingRequest request) =>
			Dispatcher.Dispatch<IPutRoleMappingRequest, PutRoleMappingRequestParameters, PutRoleMappingResponse>(
				request,
				LowLevelDispatch.SecurityPutRoleMappingDispatch<PutRoleMappingResponse>
			);

		/// <inheritdoc />
		public Task<IPutRoleMappingResponse> PutRoleMappingAsync(Name role, Func<PutRoleMappingDescriptor, IPutRoleMappingRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutRoleMappingAsync(selector.InvokeOrDefault(new PutRoleMappingDescriptor(role)), cancellationToken);

		/// <inheritdoc />
		public Task<IPutRoleMappingResponse> PutRoleMappingAsync(IPutRoleMappingRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IPutRoleMappingRequest, PutRoleMappingRequestParameters, PutRoleMappingResponse, IPutRoleMappingResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.SecurityPutRoleMappingDispatchAsync<PutRoleMappingResponse>
			);
	}
}
