using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GraphExploreResponse GraphExplore<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)
			where T : class;

		/// <inheritdoc />
		GraphExploreResponse GraphExplore(IGraphExploreRequest request);

		/// <inheritdoc />
		Task<GraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<GraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GraphExploreResponse GraphExplore<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector) where T : class =>
			GraphExplore(selector?.Invoke(new GraphExploreDescriptor<T>()));

		/// <inheritdoc />
		public GraphExploreResponse GraphExplore(IGraphExploreRequest request) =>
			DoRequest<IGraphExploreRequest, GraphExploreResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GraphExploreResponse> GraphExploreAsync<T>(
			Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			GraphExploreAsync(selector?.Invoke(new GraphExploreDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<GraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGraphExploreRequest, GraphExploreResponse, GraphExploreResponse>(request, request.RequestParameters, ct);
	}
}
