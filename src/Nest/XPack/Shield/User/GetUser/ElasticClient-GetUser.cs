using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetUserResponse GetUser(Func<GetUserDescriptor, IGetUserRequest> selector = null);

		/// <inheritdoc/>
		IGetUserResponse GetUser(IGetUserRequest request);

		/// <inheritdoc/>
		Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetUserResponse> GetUserAsync(IGetUserRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetUserResponse GetUser(Func<GetUserDescriptor, IGetUserRequest> selector = null) =>
			this.GetUser(selector.InvokeOrDefault(new GetUserDescriptor()));

		/// <inheritdoc/>
		public IGetUserResponse GetUser(IGetUserRequest request) =>
			this.Dispatcher.Dispatch<IGetUserRequest, GetUserRequestParameters, GetUserResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.ShieldGetUserDispatch<GetUserResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector = null) =>
			this.GetUserAsync(selector.InvokeOrDefault(new GetUserDescriptor()));

		/// <inheritdoc/>
		public Task<IGetUserResponse> GetUserAsync(IGetUserRequest request) =>
			this.Dispatcher.DispatchAsync<IGetUserRequest, GetUserRequestParameters, GetUserResponse, IGetUserResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.ShieldGetUserDispatchAsync<GetUserResponse>(p)
			);
	}
}
