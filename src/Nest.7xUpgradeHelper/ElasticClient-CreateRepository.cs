using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Before any snapshot or restore operation can be performed a snapshot repository should be registered in Elasticsearch.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_repositories
		/// </summary>
		/// <param name="repository">The name for the repository</param>
		/// <param name="selector">describe what the repository looks like</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CreateRepositoryResponse CreateRepository(this IElasticClient client, Name repository,
			Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector
		)
			=> client.Snapshot.CreateRepository(repository, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CreateRepositoryResponse CreateRepository(this IElasticClient client, ICreateRepositoryRequest request)
			=> client.Snapshot.CreateRepository(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CreateRepositoryResponse> CreateRepositoryAsync(this IElasticClient client, Name repository,
			Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector,
			CancellationToken ct = default
		)
			=> client.Snapshot.CreateRepositoryAsync(repository, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CreateRepositoryResponse> CreateRepositoryAsync(this IElasticClient client, ICreateRepositoryRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.CreateRepositoryAsync(request);
	}
}
