using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null);

		/// <inheritdoc />
		IForceMergeResponse ForceMerge(IForceMergeRequest request);

		/// <inheritdoc />
		Task<IForceMergeResponse> ForceMergeAsync(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IForceMergeResponse ForceMerge(Indices indices, Func<ForceMergeDescriptor, IForceMergeRequest> selector = null) =>
			ForceMerge(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)));

		/// <inheritdoc />
		public IForceMergeResponse ForceMerge(IForceMergeRequest request) =>
			DoRequest<IForceMergeRequest, ForceMergeResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IForceMergeResponse> ForceMergeAsync(
			Indices indices,
			Func<ForceMergeDescriptor, IForceMergeRequest> selector = null,
			CancellationToken ct = default
		) => ForceMergeAsync(selector.InvokeOrDefault(new ForceMergeDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<IForceMergeResponse> ForceMergeAsync(IForceMergeRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IForceMergeRequest, IForceMergeResponse, ForceMergeResponse>(request, request.RequestParameters, ct);
	}
}
