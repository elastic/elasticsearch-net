using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGraphExploreResponse GraphExplore<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)
			where T : class;

		/// <inheritdoc/>
		IGraphExploreResponse GraphExplore(IGraphExploreRequest request);

		/// <inheritdoc/>
		Task<IGraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector, CancellationToken cancellationToken = default(CancellationToken))
			where T : class;

		/// <inheritdoc/>
		Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGraphExploreResponse GraphExplore<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector) where T : class =>
			this.GraphExplore(selector?.Invoke(new GraphExploreDescriptor<T>()));

		/// <inheritdoc/>
		public IGraphExploreResponse GraphExplore(IGraphExploreRequest request) =>
			this.Dispatcher.Dispatch<IGraphExploreRequest, GraphExploreRequestParameters, GraphExploreResponse>(
				request,
				this.LowLevelDispatch.XpackGraphExploreDispatch<GraphExploreResponse>
			);

		/// <inheritdoc/>
		public Task<IGraphExploreResponse> GraphExploreAsync<T>(Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) where T : class =>
			this.GraphExploreAsync(selector?.Invoke(new GraphExploreDescriptor<T>()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGraphExploreResponse> GraphExploreAsync(IGraphExploreRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGraphExploreRequest, GraphExploreRequestParameters, GraphExploreResponse, IGraphExploreResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackGraphExploreDispatchAsync<GraphExploreResponse>
			);
	}
}
