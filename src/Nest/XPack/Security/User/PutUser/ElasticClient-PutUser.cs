using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc/>
		IPutUserResponse PutUser(IPutUserRequest request);

		/// <inheritdoc/>
		Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null) =>
			this.PutUser(selector.InvokeOrDefault(new PutUserDescriptor(username)));

		/// <inheritdoc/>
		public IPutUserResponse PutUser(IPutUserRequest request) =>
			this.Dispatcher.Dispatch<IPutUserRequest, PutUserRequestParameters, PutUserResponse>(
				request,
				this.LowLevelDispatch.XpackSecurityPutUserDispatch<PutUserResponse>
			);

		/// <inheritdoc/>
		public Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PutUserAsync(selector.InvokeOrDefault(new PutUserDescriptor(username)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutUserRequest, PutUserRequestParameters, PutUserResponse, IPutUserResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackSecurityPutUserDispatchAsync<PutUserResponse>
			);
	}
}
