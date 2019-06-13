using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		public static MigrationUpgradeResponse MigrationUpgrade(this IElasticClient client,IndexName index, Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		public static MigrationUpgradeResponse MigrationUpgrade(this IElasticClient client,IMigrationUpgradeRequest request);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		public static Task<MigrationUpgradeResponse> MigrationUpgradeAsync(this IElasticClient client,IndexName index,
			Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		public static Task<MigrationUpgradeResponse> MigrationUpgradeAsync(this IElasticClient client,IMigrationUpgradeRequest request,
			CancellationToken ct = default
		);
	}

}
