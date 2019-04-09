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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteUserResponse DeleteUser(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null) =>
			DeleteUser(selector.InvokeOrDefault(new DeleteUserDescriptor(username)));

		/// <inheritdoc />
		public IDeleteUserResponse DeleteUser(IDeleteUserRequest request) =>
			Dispatch2<IDeleteUserRequest, DeleteUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteUserResponse> DeleteUserAsync(
			Name username,
			Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null,
			CancellationToken ct = default
		) => DeleteUserAsync(selector.InvokeOrDefault(new DeleteUserDescriptor(username)), ct);

		/// <inheritdoc />
		public Task<IDeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteUserRequest, IDeleteUserResponse, DeleteUserResponse>(request, request.RequestParameters, ct);
	}
}
