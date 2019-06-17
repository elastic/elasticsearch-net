using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Add a single index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-adding
		/// </summary>
		/// <param name="request">A descriptor that describes the put alias request</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutAliasResponse PutAlias(this IElasticClient client, IPutAliasRequest request)
			=> client.Indices.PutAlias(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutAliasResponse> PutAliasAsync(this IElasticClient client, IPutAliasRequest request, CancellationToken ct = default)
			=> client.Indices.PutAliasAsync(request, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutAliasResponse PutAlias(this IElasticClient client, Indices indices, Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null
		)
			=> client.Indices.PutAlias(indices, alias, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutAliasResponse> PutAliasAsync(this IElasticClient client,
			Indices indices,
			Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.PutAliasAsync(indices, alias, selector, ct);
	}
}
