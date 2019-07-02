using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Refresh(), please update this usage.")]
		public static RefreshResponse Refresh(this IElasticClient client, Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector = null)
			=> client.Indices.Refresh(indices, selector);

		[Obsolete("Moved to client.Indices.Refresh(), please update this usage.")]
		public static RefreshResponse Refresh(this IElasticClient client, IRefreshRequest request)
			=> client.Indices.Refresh(request);

		[Obsolete("Moved to client.Indices.RefreshAsync(), please update this usage.")]
		public static Task<RefreshResponse> RefreshAsync(this IElasticClient client, Indices indices,
			Func<RefreshDescriptor, IRefreshRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.RefreshAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.RefreshAsync(), please update this usage.")]
		public static Task<RefreshResponse> RefreshAsync(this IElasticClient client, IRefreshRequest request, CancellationToken ct = default)
			=> client.Indices.RefreshAsync(request, ct);
	}
}
