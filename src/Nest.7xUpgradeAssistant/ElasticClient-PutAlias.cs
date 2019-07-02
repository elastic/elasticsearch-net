using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.PutAlias(), please update this usage.")]
		public static PutAliasResponse PutAlias(this IElasticClient client, IPutAliasRequest request)
			=> client.Indices.PutAlias(request);

		[Obsolete("Moved to client.Indices.PutAliasAsync(), please update this usage.")]
		public static Task<PutAliasResponse> PutAliasAsync(this IElasticClient client, IPutAliasRequest request, CancellationToken ct = default)
			=> client.Indices.PutAliasAsync(request, ct);

		[Obsolete("Moved to client.Indices.PutAlias(), please update this usage.")]
		public static PutAliasResponse PutAlias(this IElasticClient client, Indices indices, Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null
		)
			=> client.Indices.PutAlias(indices, alias, selector);

		[Obsolete("Moved to client.Indices.PutAliasAsync(), please update this usage.")]
		public static Task<PutAliasResponse> PutAliasAsync(this IElasticClient client,
			Indices indices,
			Name alias,
			Func<PutAliasDescriptor, IPutAliasRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.PutAliasAsync(indices, alias, selector, ct);
	}
}
