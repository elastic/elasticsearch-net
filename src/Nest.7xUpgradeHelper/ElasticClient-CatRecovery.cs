using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatRecoveryRecord> CatRecovery(this IElasticClient client,
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null
		)
			=> client.Cat.Recovery(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatRecoveryRecord> CatRecovery(this IElasticClient client, ICatRecoveryRequest request)
			=> client.Cat.Recovery(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatRecoveryRecord>> CatRecoveryAsync(this IElasticClient client,
			Func<CatRecoveryDescriptor, ICatRecoveryRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.RecoveryAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatRecoveryRecord>> CatRecoveryAsync(this IElasticClient client, ICatRecoveryRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.RecoveryAsync(request, ct);
	}
}
