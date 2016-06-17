using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutRoleResponse PutRole(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null);

		/// <inheritdoc/>
		IPutRoleResponse PutRole(IPutRoleRequest request);

		/// <inheritdoc/>
		Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
				this.LowLevelDispatch.XpackSecurityPutRoleDispatch<PutRoleResponse>
			);

		/// <inheritdoc/>
		public Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PutRoleAsync(selector.InvokeOrDefault(new PutRoleDescriptor(role)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutRoleRequest, PutRoleRequestParameters, PutRoleResponse, IPutRoleResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackSecurityPutRoleDispatchAsync<PutRoleResponse>
			);
	}
}
