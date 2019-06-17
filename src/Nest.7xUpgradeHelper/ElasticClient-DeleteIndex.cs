using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The delete index API allows to delete an existing index.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-index.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the delete index operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteIndexResponse DeleteIndex(this IElasticClient client, Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null
		)
			=> client.Indices.Delete(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteIndexResponse DeleteIndex(this IElasticClient client, IDeleteIndexRequest request)
			=> client.Indices.Delete(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteIndexResponse> DeleteIndexAsync(this IElasticClient client,
			Indices indices,
			Func<DeleteIndexDescriptor, IDeleteIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteIndexResponse> DeleteIndexAsync(this IElasticClient client, IDeleteIndexRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.DeleteAsync(request, ct);
	}
}
