using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Snapshot.VerifyRepository(), please update this usage.")]
		public static VerifyRepositoryResponse VerifyRepository(this IElasticClient client, Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null
		)
			=> client.Snapshot.VerifyRepository(repository, selector);

		[Obsolete("Moved to client.Snapshot.VerifyRepository(), please update this usage.")]
		public static VerifyRepositoryResponse VerifyRepository(this IElasticClient client, IVerifyRepositoryRequest request)
			=> client.Snapshot.VerifyRepository(request);

		[Obsolete("Moved to client.Snapshot.VerifyRepositoryAsync(), please update this usage.")]
		public static Task<VerifyRepositoryResponse> VerifyRepositoryAsync(this IElasticClient client, Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.VerifyRepositoryAsync(repository, selector, ct);

		[Obsolete("Moved to client.Snapshot.VerifyRepositoryAsync(), please update this usage.")]
		public static Task<VerifyRepositoryResponse> VerifyRepositoryAsync(this IElasticClient client, IVerifyRepositoryRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.VerifyRepositoryAsync(request, ct);
	}
}
