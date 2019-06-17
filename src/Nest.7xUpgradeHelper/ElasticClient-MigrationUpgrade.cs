using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static MigrationUpgradeResponse MigrationUpgrade(this IElasticClient client, IndexName index,
			Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null
		)
			=> client.Migration.Upgrade(index, selector);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static MigrationUpgradeResponse MigrationUpgrade(this IElasticClient client, IMigrationUpgradeRequest request)
			=> client.Migration.Upgrade(request);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<MigrationUpgradeResponse> MigrationUpgradeAsync(this IElasticClient client, IndexName index,
			Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Migration.UpgradeAsync(index, selector, ct);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<MigrationUpgradeResponse> MigrationUpgradeAsync(this IElasticClient client, IMigrationUpgradeRequest request,
			CancellationToken ct = default
		)
			=> client.Migration.UpgradeAsync(request, ct);
	}
}
