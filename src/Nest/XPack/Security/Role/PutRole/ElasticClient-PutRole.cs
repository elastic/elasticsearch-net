using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		PutRoleResponse PutRole(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null);

		/// <inheritdoc />
		PutRoleResponse PutRole(IPutRoleRequest request);

		/// <inheritdoc />
		Task<PutRoleResponse> PutRoleAsync(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<PutRoleResponse> PutRoleAsync(IPutRoleRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutRoleResponse PutRole(Name role, Func<PutRoleDescriptor, IPutRoleRequest> selector = null) =>
			PutRole(selector.InvokeOrDefault(new PutRoleDescriptor(role)));

		/// <inheritdoc />
		public PutRoleResponse PutRole(IPutRoleRequest request) =>
			DoRequest<IPutRoleRequest, PutRoleResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PutRoleResponse> PutRoleAsync(
			Name role,
			Func<PutRoleDescriptor, IPutRoleRequest> selector = null,
			CancellationToken cancellationToken = default
		) => PutRoleAsync(selector.InvokeOrDefault(new PutRoleDescriptor(role)), cancellationToken);

		/// <inheritdoc />
		public Task<PutRoleResponse> PutRoleAsync(IPutRoleRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutRoleRequest, PutRoleResponse>(request, request.RequestParameters, ct);
	}
}
