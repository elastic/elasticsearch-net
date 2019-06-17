using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Migration.Upgrade(), please update this usage.")]
		public static MigrationUpgradeResponse MigrationUpgrade(this IElasticClient client, IndexName index,
			Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null
		)
			=> client.Migration.Upgrade(index, selector);

		[Obsolete("Moved to client.Migration.Upgrade(), please update this usage.")]
		public static MigrationUpgradeResponse MigrationUpgrade(this IElasticClient client, IMigrationUpgradeRequest request)
			=> client.Migration.Upgrade(request);

		[Obsolete("Moved to client.Migration.UpgradeAsync(), please update this usage.")]
		public static Task<MigrationUpgradeResponse> MigrationUpgradeAsync(this IElasticClient client, IndexName index,
			Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Migration.UpgradeAsync(index, selector, ct);

		[Obsolete("Moved to client.Migration.UpgradeAsync(), please update this usage.")]
		public static Task<MigrationUpgradeResponse> MigrationUpgradeAsync(this IElasticClient client, IMigrationUpgradeRequest request,
			CancellationToken ct = default
		)
			=> client.Migration.UpgradeAsync(request, ct);
	}
}
