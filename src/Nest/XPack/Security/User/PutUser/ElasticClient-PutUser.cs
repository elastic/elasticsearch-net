using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc />
		IPutUserResponse PutUser(IPutUserRequest request);

		/// <inheritdoc />
		Task<IPutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IPutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null) =>
			PutUser(selector.InvokeOrDefault(new PutUserDescriptor(username)));

		/// <inheritdoc />
		public IPutUserResponse PutUser(IPutUserRequest request) =>
			Dispatch2<IPutUserRequest, PutUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPutUserResponse> PutUserAsync(
			Name username,
			Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken ct = default
		) => PutUserAsync(selector.InvokeOrDefault(new PutUserDescriptor(username)), ct);

		/// <inheritdoc />
		public Task<IPutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IPutUserRequest, IPutUserResponse, PutUserResponse>
				(request, request.RequestParameters, ct);
	}
}
