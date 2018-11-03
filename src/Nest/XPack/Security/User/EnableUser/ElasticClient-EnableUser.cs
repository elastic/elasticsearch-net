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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IEnableUserResponse> EnableUserAsync(IEnableUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IEnableUserResponse EnableUser(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null) =>
			EnableUser(selector.InvokeOrDefault(new EnableUserDescriptor().Username(username)));

		/// <inheritdoc />
		public IEnableUserResponse EnableUser(IEnableUserRequest request) =>
			Dispatcher.Dispatch<IEnableUserRequest, EnableUserRequestParameters, EnableUserResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackSecurityEnableUserDispatch<EnableUserResponse>(p)
			);

		/// <inheritdoc />
		public Task<IEnableUserResponse> EnableUserAsync(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			EnableUserAsync(selector.InvokeOrDefault(new EnableUserDescriptor().Username(username)), cancellationToken);

		/// <inheritdoc />
		public Task<IEnableUserResponse> EnableUserAsync(IEnableUserRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IEnableUserRequest, EnableUserRequestParameters, EnableUserResponse, IEnableUserResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackSecurityEnableUserDispatchAsync<EnableUserResponse>(p, c)
			);
	}
}
