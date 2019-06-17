using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpgradeResponse Upgrade(this IElasticClient client, IUpgradeRequest request)
			=> client.Indices.Upgrade(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UpgradeResponse Upgrade(this IElasticClient client, Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null)
			=> client.Indices.Upgrade(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpgradeResponse> UpgradeAsync(this IElasticClient client, IUpgradeRequest request, CancellationToken ct = default)
			=> client.Indices.UpgradeAsync(request, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UpgradeResponse> UpgradeAsync(this IElasticClient client, Indices indices,
			Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.UpgradeAsync(indices, selector, ct);
	}
}
