using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDisableUserResponse DisableUser(Name username, Func<DisableUserDescriptor, IDisableUserRequest> selector = null);

		/// <inheritdoc />
		IDisableUserResponse DisableUser(IDisableUserRequest request);

		/// <inheritdoc />
		Task<IDisableUserResponse> DisableUserAsync(Name username, Func<DisableUserDescriptor, IDisableUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDisableUserResponse> DisableUserAsync(IDisableUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDisableUserResponse DisableUser(Name username, Func<DisableUserDescriptor, IDisableUserRequest> selector = null) =>
			DisableUser(selector.InvokeOrDefault(new DisableUserDescriptor(username)));

		/// <inheritdoc />
		public IDisableUserResponse DisableUser(IDisableUserRequest request) =>
			Dispatch2<IDisableUserRequest, DisableUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDisableUserResponse> DisableUserAsync(
			Name username,
			Func<DisableUserDescriptor, IDisableUserRequest> selector = null,
			CancellationToken ct = default
		) => DisableUserAsync(selector.InvokeOrDefault(new DisableUserDescriptor(username)), ct);

		/// <inheritdoc />
		public Task<IDisableUserResponse> DisableUserAsync(IDisableUserRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDisableUserRequest, IDisableUserResponse, DisableUserResponse>(request, request.RequestParameters, ct);
	}
}
