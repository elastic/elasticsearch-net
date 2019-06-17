using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Delete a repository, if you have ongoing restore operations be sure to delete the indices being restored into first.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_repositories
		/// </summary>
		/// <param name="repositories">The names of the repositories</param>
		/// <param name="selector">Optionaly provide the delete operation with more details</param>
		/// >
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteRepositoryResponse DeleteRepository(this IElasticClient client, Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null
		)
			=> client.Snapshot.DeleteRepository(repositories, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteRepositoryResponse DeleteRepository(this IElasticClient client, IDeleteRepositoryRequest request)
			=> client.Snapshot.DeleteRepository(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteRepositoryResponse> DeleteRepositoryAsync(this IElasticClient client, Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.DeleteRepositoryAsync(repositories, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteRepositoryResponse> DeleteRepositoryAsync(this IElasticClient client, IDeleteRepositoryRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.DeleteRepositoryAsync(request);
	}
}
