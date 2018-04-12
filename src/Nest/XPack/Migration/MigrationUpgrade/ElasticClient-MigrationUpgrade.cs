using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		IMigrationUpgradeResponse MigrationUpgrade(IndexName index, Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		IMigrationUpgradeResponse MigrationUpgrade(IMigrationUpgradeRequest request);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		Task<IMigrationUpgradeResponse> MigrationUpgradeAsync(IndexName index, Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		Task<IMigrationUpgradeResponse> MigrationUpgradeAsync(IMigrationUpgradeRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMigrationUpgradeResponse MigrationUpgrade(IndexName index, Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null) =>
			this.MigrationUpgrade(selector.InvokeOrDefault(new MigrationUpgradeDescriptor(index)));

		/// <inheritdoc/>
		public IMigrationUpgradeResponse MigrationUpgrade(IMigrationUpgradeRequest request) =>
			this.Dispatcher.Dispatch<IMigrationUpgradeRequest, MigrationUpgradeRequestParameters, MigrationUpgradeResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackMigrationUpgradeDispatch<MigrationUpgradeResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IMigrationUpgradeResponse> MigrationUpgradeAsync(IndexName index, Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.MigrationUpgradeAsync(selector.InvokeOrDefault(new MigrationUpgradeDescriptor(index)), cancellationToken);

		/// <inheritdoc/>
		public Task<IMigrationUpgradeResponse> MigrationUpgradeAsync(IMigrationUpgradeRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IMigrationUpgradeRequest, MigrationUpgradeRequestParameters, MigrationUpgradeResponse, IMigrationUpgradeResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMigrationUpgradeDispatchAsync<MigrationUpgradeResponse>(p, c)
			);
	}
}
