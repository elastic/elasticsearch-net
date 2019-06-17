using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.DeleteRepository(), please update this usage.")]
		public static DeleteRepositoryResponse DeleteRepository(this IElasticClient client, Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null
		)
			=> client.Snapshot.DeleteRepository(repositories, selector);

		[Obsolete("Moved to client.Snapshot.DeleteRepository(), please update this usage.")]
		public static DeleteRepositoryResponse DeleteRepository(this IElasticClient client, IDeleteRepositoryRequest request)
			=> client.Snapshot.DeleteRepository(request);

		[Obsolete("Moved to client.Snapshot.DeleteRepositoryAsync(), please update this usage.")]
		public static Task<DeleteRepositoryResponse> DeleteRepositoryAsync(this IElasticClient client, Names repositories,
			Func<DeleteRepositoryDescriptor, IDeleteRepositoryRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.DeleteRepositoryAsync(repositories, selector, ct);

		[Obsolete("Moved to client.Snapshot.DeleteRepositoryAsync(), please update this usage.")]
		public static Task<DeleteRepositoryResponse> DeleteRepositoryAsync(this IElasticClient client, IDeleteRepositoryRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.DeleteRepositoryAsync(request, ct);
	}
}
