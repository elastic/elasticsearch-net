using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetUserResponse GetUser(Func<GetUserDescriptor, IGetUserRequest> selector = null);

		/// <inheritdoc/>
		IGetUserResponse GetUser(IGetUserRequest request);

		/// <inheritdoc/>
		Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetUserResponse> GetUserAsync(IGetUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
				(p, d) =>this.LowLevelDispatch.XpackSecurityGetUserDispatch<GetUserResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetUserAsync(selector.InvokeOrDefault(new GetUserDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetUserResponse> GetUserAsync(IGetUserRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetUserRequest, GetUserRequestParameters, GetUserResponse, IGetUserResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityGetUserDispatchAsync<GetUserResponse>(p, c)
			);
	}
}
