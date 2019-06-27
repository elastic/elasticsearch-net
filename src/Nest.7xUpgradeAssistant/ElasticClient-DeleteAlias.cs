using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.DeleteAlias(), please update this usage.")]
		public static DeleteAliasResponse DeleteAlias(this IElasticClient client, IDeleteAliasRequest request)
			=> client.Indices.DeleteAlias(request);

		[Obsolete("Moved to client.Indices.DeleteAliasAsync(), please update this usage.")]
		public static Task<DeleteAliasResponse> DeleteAliasAsync(this IElasticClient client, IDeleteAliasRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteAliasAsync(request, ct);

		[Obsolete("Moved to client.Indices.DeleteAlias(), please update this usage.")]
		public static DeleteAliasResponse DeleteAlias(this IElasticClient client, Indices indices, Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null
		)
			=> client.Indices.DeleteAlias(indices, names, selector);

		[Obsolete("Moved to client.Indices.DeleteAliasAsync(), please update this usage.")]
		public static Task<DeleteAliasResponse> DeleteAliasAsync(this IElasticClient client,
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteAliasAsync(indices, names, selector, ct);
	}
}
