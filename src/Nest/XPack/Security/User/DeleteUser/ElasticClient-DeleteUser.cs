using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteUserResponse DeleteUser(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null);

		/// <inheritdoc/>
		IDeleteUserResponse DeleteUser(IDeleteUserRequest request);

		/// <inheritdoc/>
		Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteUserResponse DeleteUser(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null) =>
			this.DeleteUser(selector.InvokeOrDefault(new DeleteUserDescriptor(username)));

		/// <inheritdoc/>
		public IDeleteUserResponse DeleteUser(IDeleteUserRequest request) =>
			this.Dispatcher.Dispatch<IDeleteUserRequest, DeleteUserRequestParameters, DeleteUserResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackSecurityDeleteUserDispatch<DeleteUserResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeleteUserAsync(selector.InvokeOrDefault(new DeleteUserDescriptor(username)), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeleteUserRequest, DeleteUserRequestParameters, DeleteUserResponse, IDeleteUserResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityDeleteUserDispatchAsync<DeleteUserResponse>(p, c)
			);
	}
}
