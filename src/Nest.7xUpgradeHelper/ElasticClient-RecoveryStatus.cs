using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RecoveryStatusResponse RecoveryStatus(this IElasticClient client, Indices indices,
			Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null
		)
			=> client.Indices.RecoveryStatus(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RecoveryStatusResponse RecoveryStatus(this IElasticClient client, IRecoveryStatusRequest request)
			=> client.Indices.RecoveryStatus(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RecoveryStatusResponse> RecoveryStatusAsync(this IElasticClient client, Indices indices,
			Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.RecoveryStatusAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RecoveryStatusResponse> RecoveryStatusAsync(this IElasticClient client, IRecoveryStatusRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.RecoveryStatusAsync(request, ct);
	}
}
