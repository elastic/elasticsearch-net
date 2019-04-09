using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IEnableUserResponse EnableUser(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null);

		/// <inheritdoc />
		IEnableUserResponse EnableUser(IEnableUserRequest request);

		/// <inheritdoc />
		Task<IEnableUserResponse> EnableUserAsync(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IEnableUserResponse> EnableUserAsync(IEnableUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IEnableUserResponse EnableUser(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null) =>
			EnableUser(selector.InvokeOrDefault(new EnableUserDescriptor(username)));

		/// <inheritdoc />
		public IEnableUserResponse EnableUser(IEnableUserRequest request) =>
			Dispatch2<IEnableUserRequest, EnableUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IEnableUserResponse> EnableUserAsync(
			Name username,
			Func<EnableUserDescriptor, IEnableUserRequest> selector = null,
			CancellationToken ct = default
		) => EnableUserAsync(selector.InvokeOrDefault(new EnableUserDescriptor(username)), ct);

		/// <inheritdoc />
		public Task<IEnableUserResponse> EnableUserAsync(IEnableUserRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IEnableUserRequest, IEnableUserResponse, EnableUserResponse>(request, request.RequestParameters, ct);
	}
}
