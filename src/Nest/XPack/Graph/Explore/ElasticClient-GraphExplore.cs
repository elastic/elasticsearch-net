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
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;

		/// <inheritdoc />
		Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGraphExploreResponse GraphExplore<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector) where T : class =>
			GraphExplore(selector?.Invoke(new GraphExploreDescriptor<T>()));

		/// <inheritdoc />
		public IGraphExploreResponse GraphExplore(IGraphExploreRequest request) =>
			Dispatcher.Dispatch<IGraphExploreRequest, GraphExploreRequestParameters, GraphExploreResponse>(
				request,
				LowLevelDispatch.GraphExploreDispatch<GraphExploreResponse>
			);

		/// <inheritdoc />
		public Task<IGraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		) where T : class =>
			GraphExploreAsync(selector?.Invoke(new GraphExploreDescriptor<T>()), cancellationToken);

		/// <inheritdoc />
		public Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGraphExploreRequest, GraphExploreRequestParameters, GraphExploreResponse, IGraphExploreResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.GraphExploreDispatchAsync<GraphExploreResponse>
			);
	}
}
