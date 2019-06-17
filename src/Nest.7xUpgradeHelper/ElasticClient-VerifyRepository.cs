using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static VerifyRepositoryResponse VerifyRepository(this IElasticClient client, Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null
		)
			=> client.Snapshot.VerifyRepository(repository, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static VerifyRepositoryResponse VerifyRepository(this IElasticClient client, IVerifyRepositoryRequest request)
			=> client.Snapshot.VerifyRepository(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<VerifyRepositoryResponse> VerifyRepositoryAsync(this IElasticClient client, Name repository,
			Func<VerifyRepositoryDescriptor, IVerifyRepositoryRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Snapshot.VerifyRepositoryAsync(repository, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<VerifyRepositoryResponse> VerifyRepositoryAsync(this IElasticClient client, IVerifyRepositoryRequest request,
			CancellationToken ct = default
		)
			=> client.Snapshot.VerifyRepositoryAsync(request, ct);
	}
}
