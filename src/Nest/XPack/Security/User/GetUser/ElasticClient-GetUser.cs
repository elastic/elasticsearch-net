using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetUserResponse GetUser(Func<GetUserDescriptor, IGetUserRequest> selector = null);

		/// <inheritdoc />
		IGetUserResponse GetUser(IGetUserRequest request);

		/// <inheritdoc />
		Task<IGetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetUserResponse> GetUserAsync(IGetUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetUserResponse GetUser(Func<GetUserDescriptor, IGetUserRequest> selector = null) =>
			GetUser(selector.InvokeOrDefault(new GetUserDescriptor()));

		/// <inheritdoc />
		public IGetUserResponse GetUser(IGetUserRequest request) =>
			DoRequest<IGetUserRequest, GetUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetUserResponse> GetUserAsync(
			Func<GetUserDescriptor, IGetUserRequest> selector = null,
			CancellationToken ct = default
		) => GetUserAsync(selector.InvokeOrDefault(new GetUserDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetUserResponse> GetUserAsync(IGetUserRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetUserRequest, IGetUserResponse, GetUserResponse>(request, request.RequestParameters, ct);
	}
}
