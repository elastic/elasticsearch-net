using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		DisableUserResponse DisableUser(Name username, Func<DisableUserDescriptor, IDisableUserRequest> selector = null);

		/// <inheritdoc />
		DisableUserResponse DisableUser(IDisableUserRequest request);

		/// <inheritdoc />
		Task<DisableUserResponse> DisableUserAsync(Name username, Func<DisableUserDescriptor, IDisableUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DisableUserResponse> DisableUserAsync(IDisableUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DisableUserResponse DisableUser(Name username, Func<DisableUserDescriptor, IDisableUserRequest> selector = null) =>
			DisableUser(selector.InvokeOrDefault(new DisableUserDescriptor(username)));

		/// <inheritdoc />
		public DisableUserResponse DisableUser(IDisableUserRequest request) =>
			DoRequest<IDisableUserRequest, DisableUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DisableUserResponse> DisableUserAsync(
			Name username,
			Func<DisableUserDescriptor, IDisableUserRequest> selector = null,
			CancellationToken ct = default
		) => DisableUserAsync(selector.InvokeOrDefault(new DisableUserDescriptor(username)), ct);

		/// <inheritdoc />
		public Task<DisableUserResponse> DisableUserAsync(IDisableUserRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDisableUserRequest, DisableUserResponse, DisableUserResponse>(request, request.RequestParameters, ct);
	}
}
