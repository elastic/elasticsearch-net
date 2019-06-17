using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GraphExploreResponse GraphExplore<T>(this IElasticClient client, Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)
			where T : class => client.Graph.Explore(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GraphExploreResponse GraphExplore(this IElasticClient client, IGraphExploreRequest request)
			=> client.Graph.Explore(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GraphExploreResponse> GraphExploreAsync<T>(this IElasticClient client,
			Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector,
			CancellationToken ct = default
		)
			where T : class => client.Graph.ExploreAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GraphExploreResponse> GraphExploreAsync(this IElasticClient client, IGraphExploreRequest request,
			CancellationToken ct = default
		)
			=> client.Graph.ExploreAsync(request, ct);
	}
}
