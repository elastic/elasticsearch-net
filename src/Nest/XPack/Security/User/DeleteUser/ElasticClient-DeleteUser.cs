using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IDeleteUserResponse DeleteUser(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null);

		/// <inheritdoc/>
		IDeleteUserResponse DeleteUser(IDeleteUserRequest request);

		/// <inheritdoc/>
		Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request);
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
				(p, d) =>this.LowLevelDispatch.ShieldDeleteUserDispatch<DeleteUserResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null) =>
			this.DeleteUserAsync(selector.InvokeOrDefault(new DeleteUserDescriptor(username)));

		/// <inheritdoc/>
		public Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request) =>
			this.Dispatcher.DispatchAsync<IDeleteUserRequest, DeleteUserRequestParameters, DeleteUserResponse, IDeleteUserResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.ShieldDeleteUserDispatchAsync<DeleteUserResponse>(p)
			);
	}
}
