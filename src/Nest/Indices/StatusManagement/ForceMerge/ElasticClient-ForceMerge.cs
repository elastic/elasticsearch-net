using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		ForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null);

		/// <inheritdoc />
		ForceMergeResponse ForceMerge(IForceMergeRequest request);

		/// <inheritdoc />
		Task<ForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ForceMergeResponse> ForceMergeAsync(IForceMergeRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null) =>
			ForceMerge(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)));

		/// <inheritdoc />
		public ForceMergeResponse ForceMerge(IForceMergeRequest request) =>
			DoRequest<IForceMergeRequest, ForceMergeResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ForceMergeResponse> ForceMergeAsync(
			Indices indices,
			Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken ct = default
		) => ForceMergeAsync(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<ForceMergeResponse> ForceMergeAsync(IForceMergeRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IForceMergeRequest, ForceMergeResponse, ForceMergeResponse>(request, request.RequestParameters, ct);
	}
}
