using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPutRoleResponse PutRole(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null);

		/// <inheritdoc />
		IPutRoleResponse PutRole(IPutRoleRequest request);

		/// <inheritdoc />
		Task<IPutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutRoleResponse PutRole(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null) =>
			PutRole(selector.InvokeOrDefault(new PutRoleDescriptor(role)));

		/// <inheritdoc />
		public IPutRoleResponse PutRole(IPutRoleRequest request) =>
			Dispatch2<IPutRoleRequest, PutRoleResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPutRoleResponse> PutRoleAsync(
			Name role,
			Func<PutRoleDescriptor, IPutRoleRequest> selector = null,
			CancellationToken cancellationToken = default
		) => PutRoleAsync(selector.InvokeOrDefault(new PutRoleDescriptor(role)), cancellationToken);

		/// <inheritdoc />
		public Task<IPutRoleResponse> PutRoleAsync(IPutRoleRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IPutRoleRequest, IPutRoleResponse, PutRoleResponse>(request, request.RequestParameters, ct);
	}
}
