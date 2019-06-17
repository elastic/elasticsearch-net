using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Delete an index alias
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#deleting
		/// </summary>
		/// <param name="request">A descriptor that describes the delete alias request</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteAliasResponse DeleteAlias(this IElasticClient client, IDeleteAliasRequest request)
			=> client.Indices.DeleteAlias(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteAliasResponse> DeleteAliasAsync(this IElasticClient client, IDeleteAliasRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteAliasAsync(request, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteAliasResponse DeleteAlias(this IElasticClient client, Indices indices, Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null
		)
			=> client.Indices.DeleteAlias(indices, names, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteAliasResponse> DeleteAliasAsync(this IElasticClient client,
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteAliasAsync(indices, names, selector, ct);
	}
}
