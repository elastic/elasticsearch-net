using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IDeleteUserResponse DeleteUser(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null);

		/// <inheritdoc />
		IDeleteUserResponse DeleteUser(IDeleteUserRequest request);

		/// <inheritdoc />
		Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteUserResponse DeleteUser(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null) =>
			DeleteUser(selector.InvokeOrDefault(new DeleteUserDescriptor(username)));

		/// <inheritdoc />
		public IDeleteUserResponse DeleteUser(IDeleteUserRequest request) =>
			Dispatcher.Dispatch<IDeleteUserRequest, DeleteUserRequestParameters, DeleteUserResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityDeleteUserDispatch<DeleteUserResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteUserAsync(selector.InvokeOrDefault(new DeleteUserDescriptor(username)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IDeleteUserRequest, DeleteUserRequestParameters, DeleteUserResponse, IDeleteUserResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SecurityDeleteUserDispatchAsync<DeleteUserResponse>(p, c)
			);
	}
}
