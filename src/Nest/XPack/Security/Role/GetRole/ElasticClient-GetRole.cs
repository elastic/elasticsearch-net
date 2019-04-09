using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetRoleResponse GetRole(Func<GetRoleDescriptor, IGetRoleRequest> selector = null);

		/// <inheritdoc />
		IGetRoleResponse GetRole(IGetRoleRequest request);

		/// <inheritdoc />
		Task<IGetRoleResponse> GetRoleAsync(Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetRoleResponse GetRole(Func<GetRoleDescriptor, IGetRoleRequest> selector = null) =>
			GetRole(selector.InvokeOrDefault(new GetRoleDescriptor()));

		/// <inheritdoc />
		public IGetRoleResponse GetRole(IGetRoleRequest request) =>
			Dispatch2<IGetRoleRequest, GetRoleResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetRoleResponse> GetRoleAsync(
			Func<GetRoleDescriptor, IGetRoleRequest> selector = null,
			CancellationToken ct = default
		) => GetRoleAsync(selector.InvokeOrDefault(new GetRoleDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetRoleResponse> GetRoleAsync(IGetRoleRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetRoleRequest, IGetRoleResponse, GetRoleResponse>(request, request.RequestParameters, ct);
	}
}
