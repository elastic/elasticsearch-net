using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IEnableUserResponse EnableUser(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null);

		/// <inheritdoc/>
		IEnableUserResponse EnableUser(IEnableUserRequest request);

		/// <inheritdoc/>
		Task<IEnableUserResponse> EnableUserAsync(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IEnableUserResponse> EnableUserAsync(IEnableUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IEnableUserResponse EnableUser(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null) =>
			this.EnableUser(selector.InvokeOrDefault(new EnableUserDescriptor().Username(username)));

		/// <inheritdoc/>
		public IEnableUserResponse EnableUser(IEnableUserRequest request) =>
			this.Dispatcher.Dispatch<IEnableUserRequest, EnableUserRequestParameters, EnableUserResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackSecurityEnableUserDispatch<EnableUserResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IEnableUserResponse> EnableUserAsync(Name username, Func<EnableUserDescriptor, IEnableUserRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.EnableUserAsync(selector.InvokeOrDefault(new EnableUserDescriptor().Username(username)), cancellationToken);

		/// <inheritdoc/>
		public Task<IEnableUserResponse> EnableUserAsync(IEnableUserRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IEnableUserRequest, EnableUserRequestParameters, EnableUserResponse, IEnableUserResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityEnableUserDispatchAsync<EnableUserResponse>(p, c)
			);
	}
}
