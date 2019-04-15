using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		EnableUserResponse EnableUser(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null);

		/// <inheritdoc />
		EnableUserResponse EnableUser(IEnableUserRequest request);

		/// <inheritdoc />
		Task<EnableUserResponse> EnableUserAsync(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<EnableUserResponse> EnableUserAsync(IEnableUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public EnableUserResponse EnableUser(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null) =>
			EnableUser(selector.InvokeOrDefault(new EnableUserDescriptor(username)));

		/// <inheritdoc />
		public EnableUserResponse EnableUser(IEnableUserRequest request) =>
			DoRequest<IEnableUserRequest, EnableUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<EnableUserResponse> EnableUserAsync(
			Name username,
			Func<EnableUserDescriptor, IEnableUserRequest> selector = null,
			CancellationToken ct = default
		) => EnableUserAsync(selector.InvokeOrDefault(new EnableUserDescriptor(username)), ct);

		/// <inheritdoc />
		public Task<EnableUserResponse> EnableUserAsync(IEnableUserRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IEnableUserRequest, EnableUserResponse>(request, request.RequestParameters, ct);
	}
}
