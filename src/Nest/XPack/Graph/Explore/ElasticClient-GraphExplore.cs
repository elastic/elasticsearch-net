using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGraphExploreResponse GraphExplore<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)
			where T : class;

		/// <inheritdoc />
		IGraphExploreResponse GraphExplore(IGraphExploreRequest request);

		/// <inheritdoc />
		Task<IGraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGraphExploreResponse GraphExplore<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector) where T : class =>
			GraphExplore(selector?.Invoke(new GraphExploreDescriptor<T>()));

		/// <inheritdoc />
		public IGraphExploreResponse GraphExplore(IGraphExploreRequest request) =>
			DoRequest<IGraphExploreRequest, GraphExploreResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGraphExploreResponse> GraphExploreAsync<T>(
			Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			GraphExploreAsync(selector?.Invoke(new GraphExploreDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGraphExploreRequest, IGraphExploreResponse, GraphExploreResponse>(request, request.RequestParameters, ct);
	}
}
