using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.BulkAlias(), please update this usage.")]
		public static BulkAliasResponse Alias(this IElasticClient client, Func<BulkAliasDescriptor, IBulkAliasRequest> selector)
			=> client.Indices.BulkAlias(selector);

		[Obsolete("Moved to client.Indices.BulkAlias(), please update this usage.")]
		public static BulkAliasResponse Alias(this IElasticClient client, IBulkAliasRequest request)
			=> client.Indices.BulkAlias(request);

		[Obsolete("Moved to client.Indices.BulkAliasAsync(), please update this usage.")]
		public static Task<BulkAliasResponse> AliasAsync(this IElasticClient client, Func<BulkAliasDescriptor, IBulkAliasRequest> selector,
			CancellationToken ct = default
		)
			=> client.Indices.BulkAliasAsync(selector, ct);

		[Obsolete("Moved to client.Indices.BulkAliasAsync(), please update this usage.")]
		public static Task<BulkAliasResponse> AliasAsync(this IElasticClient client, IBulkAliasRequest request, CancellationToken ct = default)
			=> client.Indices.BulkAliasAsync(request, ct);
	}
}
