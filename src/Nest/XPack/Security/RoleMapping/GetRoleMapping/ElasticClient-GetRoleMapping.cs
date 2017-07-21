using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetRoleMappingResponse GetRoleMapping(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null);

		/// <inheritdoc/>
		IGetRoleMappingResponse GetRoleMapping(IGetRoleMappingRequest request);

		/// <inheritdoc/>
		Task<IGetRoleMappingResponse> GetRoleMappingAsync(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetRoleMappingResponse> GetRoleMappingAsync(IGetRoleMappingRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetRoleMappingResponse GetRoleMapping(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null) =>
			this.GetRoleMapping(selector.InvokeOrDefault(new GetRoleMappingDescriptor()));

		/// <inheritdoc/>
		public IGetRoleMappingResponse GetRoleMapping(IGetRoleMappingRequest request) =>
			this.Dispatcher.Dispatch<IGetRoleMappingRequest, GetRoleMappingRequestParameters, GetRoleMappingResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackSecurityGetRoleMappingDispatch<GetRoleMappingResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetRoleMappingResponse> GetRoleMappingAsync(Func<GetRoleMappingDescriptor, IGetRoleMappingRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetRoleMappingAsync(selector.InvokeOrDefault(new GetRoleMappingDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetRoleMappingResponse> GetRoleMappingAsync(IGetRoleMappingRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetRoleMappingRequest, GetRoleMappingRequestParameters, GetRoleMappingResponse, IGetRoleMappingResponse>(
				request,
				cancellationToken,
				(p,d,c) => this.LowLevelDispatch.XpackSecurityGetRoleMappingDispatchAsync<GetRoleMappingResponse>(p, c)
			);
	}
}
