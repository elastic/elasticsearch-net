using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetRoleResponse GetRole(Func<GetRoleDescriptor, IGetRoleRequest> selector = null);

		/// <inheritdoc/>
		IGetRoleResponse GetRole(IGetRoleRequest request);

		/// <inheritdoc/>
		Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetRoleResponse GetRole(Func<GetRoleDescriptor, IGetRoleRequest> selector = null) =>
			this.GetRole(selector.InvokeOrDefault(new GetRoleDescriptor()));

		/// <inheritdoc/>
		public IGetRoleResponse GetRole(IGetRoleRequest request) =>
			this.Dispatcher.Dispatch<IGetRoleRequest, GetRoleRequestParameters, GetRoleResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.ShieldGetRoleDispatch<GetRoleResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector = null) =>
			this.GetRoleAsync(selector.InvokeOrDefault(new GetRoleDescriptor()));

		/// <inheritdoc/>
		public Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request) =>
			this.Dispatcher.DispatchAsync<IGetRoleRequest, GetRoleRequestParameters, GetRoleResponse, IGetRoleResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.ShieldGetRoleDispatchAsync<GetRoleResponse>(p)
			);
	}
}
