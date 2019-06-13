using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static CreateRepositoryResponse CreateRepository(this IElasticClient client,Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector);

		/// <inheritdoc />
		public static CreateRepositoryResponse CreateRepository(this IElasticClient client,ICreateRepositoryRequest request);

		/// <inheritdoc />
		public static Task<CreateRepositoryResponse> CreateRepositoryAsync(this IElasticClient client,Name repository, Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CreateRepositoryResponse> CreateRepositoryAsync(this IElasticClient client,ICreateRepositoryRequest request,
			CancellationToken ct = default
		);
	}

}
