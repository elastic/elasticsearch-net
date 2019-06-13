using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static GraphExploreResponse GraphExplore<T>(this IElasticClient client,Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector)
			where T : class;

		/// <inheritdoc />
		public static GraphExploreResponse GraphExplore(this IElasticClient client,IGraphExploreRequest request);

		/// <inheritdoc />
		public static Task<GraphExploreResponse> GraphExploreAsync<T>(this IElasticClient client,Func<GraphExploreDescriptor<T>, IGraphExploreRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		public static Task<GraphExploreResponse> GraphExploreAsync(this IElasticClient client,IGraphExploreRequest request, CancellationToken ct = default);
	}

}
