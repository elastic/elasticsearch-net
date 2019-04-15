using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetUserResponse GetUser(Func<GetUserDescriptor, IGetUserRequest> selector = null);

		/// <inheritdoc />
		GetUserResponse GetUser(IGetUserRequest request);

		/// <inheritdoc />
		Task<GetUserResponse> GetUserAsync(Func<GetUserDescriptor, IGetUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetUserResponse> GetUserAsync(IGetUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetUserResponse GetUser(Func<GetUserDescriptor, IGetUserRequest> selector = null) =>
			GetUser(selector.InvokeOrDefault(new GetUserDescriptor()));

		/// <inheritdoc />
		public GetUserResponse GetUser(IGetUserRequest request) =>
			DoRequest<IGetUserRequest, GetUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetUserResponse> GetUserAsync(
			Func<GetUserDescriptor, IGetUserRequest> selector = null,
			CancellationToken ct = default
		) => GetUserAsync(selector.InvokeOrDefault(new GetUserDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetUserResponse> GetUserAsync(IGetUserRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetUserRequest, GetUserResponse>(request, request.RequestParameters, ct);
	}
}
