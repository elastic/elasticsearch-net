using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetRoleResponse GetRole(Func<GetRoleDescriptor, IGetRoleRequest> selector = null);

		/// <inheritdoc />
		GetRoleResponse GetRole(IGetRoleRequest request);

		/// <inheritdoc />
		Task<GetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetRoleResponse> GetRoleAsync(IGetRoleRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetRoleResponse GetRole(Func<GetRoleDescriptor, IGetRoleRequest> selector = null) =>
			GetRole(selector.InvokeOrDefault(new GetRoleDescriptor()));

		/// <inheritdoc />
		public GetRoleResponse GetRole(IGetRoleRequest request) =>
			DoRequest<IGetRoleRequest, GetRoleResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetRoleResponse> GetRoleAsync(
			Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken ct = default
		) => GetRoleAsync(selector.InvokeOrDefault(new GetRoleDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetRoleResponse> GetRoleAsync(IGetRoleRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetRoleRequest, GetRoleResponse>(request, request.RequestParameters, ct);
	}
}
