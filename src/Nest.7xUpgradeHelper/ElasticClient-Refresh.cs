using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The refresh API allows to explicitly refresh one or more index, making all operations performed since the last refresh
		/// available for search. The (near) real-time capabilities depend on the index engine used.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-refresh.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-refresh.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the refresh operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RefreshResponse Refresh(this IElasticClient client, Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector = null)
			=> client.Indices.Refresh(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RefreshResponse Refresh(this IElasticClient client, IRefreshRequest request)
			=> client.Indices.Refresh(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RefreshResponse> RefreshAsync(this IElasticClient client, Indices indices,
			Func<RefreshDescriptor, IRefreshRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.RefreshAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RefreshResponse> RefreshAsync(this IElasticClient client, IRefreshRequest request, CancellationToken ct = default)
			=> client.Indices.RefreshAsync(request, ct);
	}
}
