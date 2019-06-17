using System;
using System.Threading;
using System.Threading.Tasks;
using static Nest.Infer;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpgradeStatusResponse UpgradeStatus(this IElasticClient client, IUpgradeStatusRequest request)
			=> client.Indices.UpgradeStatus(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpgradeStatusResponse UpgradeStatus(this IElasticClient client,
			Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null
		)
			=> client.Indices.UpgradeStatus(AllIndices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpgradeStatusResponse> UpgradeStatusAsync(this IElasticClient client, IUpgradeStatusRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.UpgradeStatusAsync(request, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpgradeStatusResponse> UpgradeStatusAsync(this IElasticClient client,
			Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.UpgradeStatusAsync(AllIndices, selector, ct);
	}
}
