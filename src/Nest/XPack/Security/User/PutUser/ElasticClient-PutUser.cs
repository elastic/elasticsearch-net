using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		PutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null);

		/// <inheritdoc />
		PutUserResponse PutUser(IPutUserRequest request);

		/// <inheritdoc />
		Task<PutUserResponse> PutUserAsync(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<PutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutUserResponse PutUser(Name username, Func<PutUserDescriptor, IPutUserRequest> selector = null) =>
			PutUser(selector.InvokeOrDefault(new PutUserDescriptor(username)));

		/// <inheritdoc />
		public PutUserResponse PutUser(IPutUserRequest request) =>
			DoRequest<IPutUserRequest, PutUserResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PutUserResponse> PutUserAsync(
			Name username,
			Func<PutUserDescriptor, IPutUserRequest> selector = null,
			CancellationToken ct = default
		) => PutUserAsync(selector.InvokeOrDefault(new PutUserDescriptor(username)), ct);

		/// <inheritdoc />
		public Task<PutUserResponse> PutUserAsync(IPutUserRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutUserRequest, PutUserResponse, PutUserResponse>
				(request, request.RequestParameters, ct);
	}
}
