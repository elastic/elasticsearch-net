using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		DeleteUserResponse DeleteUser(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null);

		/// <inheritdoc />
		DeleteUserResponse DeleteUser(IDeleteUserRequest request);

		/// <inheritdoc />
		Task<DeleteUserResponse> DeleteUserAsync(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<DeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteUserResponse DeleteUser(Name username, Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null) =>
			DeleteUser(selector.InvokeOrDefault(new DeleteUserDescriptor(username)));

		/// <inheritdoc />
		public DeleteUserResponse DeleteUser(IDeleteUserRequest request) =>
			DoRequest<IDeleteUserRequest, DeleteUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteUserResponse> DeleteUserAsync(
			Name username,
			Func<DeleteUserDescriptor, IDeleteUserRequest> selector = null,
			CancellationToken ct = default
		) => DeleteUserAsync(selector.InvokeOrDefault(new DeleteUserDescriptor(username)), ct);

		/// <inheritdoc />
		public Task<DeleteUserResponse> DeleteUserAsync(IDeleteUserRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteUserRequest, DeleteUserResponse, DeleteUserResponse>(request, request.RequestParameters, ct);
	}
}
