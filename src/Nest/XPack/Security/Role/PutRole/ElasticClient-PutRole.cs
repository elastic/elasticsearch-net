using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutRoleResponse PutRole(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null);

		/// <inheritdoc/>
		IPutRoleResponse PutRole(IPutRoleRequest request);

		/// <inheritdoc/>
		Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null);

		/// <inheritdoc/>
		Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutRoleResponse PutRole(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null) =>
			this.PutRole(selector.InvokeOrDefault(new PutRoleDescriptor(role)));

		/// <inheritdoc/>
		public IPutRoleResponse PutRole(IPutRoleRequest request) =>
			this.Dispatcher.Dispatch<IPutRoleRequest, PutRoleRequestParameters, PutRoleResponse>(
				request,
				this.LowLevelDispatch.ShieldPutRoleDispatch<PutRoleResponse>
			);

		/// <inheritdoc/>
		public Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null) =>
			this.PutRoleAsync(selector.InvokeOrDefault(new PutRoleDescriptor(role)));

		/// <inheritdoc/>
		public Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request) =>
			this.Dispatcher.DispatchAsync<IPutRoleRequest, PutRoleRequestParameters, PutRoleResponse, IPutRoleResponse>(
				request,
				this.LowLevelDispatch.ShieldPutRoleDispatchAsync<PutRoleResponse>
			);
	}
}
