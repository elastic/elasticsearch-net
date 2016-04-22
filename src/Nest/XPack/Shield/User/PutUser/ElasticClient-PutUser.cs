using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc/>
		IPutUserResponse PutUser(IPutUserRequest request);

		/// <inheritdoc/>
		Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc/>
		Task<IPutUserResponse> PutUserAsync(IPutUserRequest request);
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
				this.LowLevelDispatch.ShieldPutUserDispatch<PutUserResponse>
			);

		/// <inheritdoc/>
		public Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null) =>
			this.PutUserAsync(selector.InvokeOrDefault(new PutUserDescriptor(username)));

		/// <inheritdoc/>
		public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request) =>
			this.Dispatcher.DispatchAsync<IPutUserRequest, PutUserRequestParameters, PutUserResponse, IPutUserResponse>(
				request,
				this.LowLevelDispatch.ShieldPutUserDispatchAsync<PutUserResponse>
			);
	}
}
