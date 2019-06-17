using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it.
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked
		/// for read/write operations.
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html</a>
		/// </summary>
		/// <param name="selector">A descriptor thata describes the close index operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CloseIndexResponse CloseIndex(this IElasticClient client, Indices indices,
			Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null
		)
			=> client.Indices.Close(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CloseIndexResponse CloseIndex(this IElasticClient client, ICloseIndexRequest request)
			=> client.Indices.Close(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CloseIndexResponse> CloseIndexAsync(this IElasticClient client,
			Indices indices,
			Func<CloseIndexDescriptor, ICloseIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.CloseAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CloseIndexResponse> CloseIndexAsync(this IElasticClient client, ICloseIndexRequest request, CancellationToken ct = default)
			=> client.Indices.CloseAsync(request, ct);
	}
}
