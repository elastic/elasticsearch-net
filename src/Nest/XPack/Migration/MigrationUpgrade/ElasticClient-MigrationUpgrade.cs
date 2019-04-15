using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		MigrationUpgradeResponse MigrationUpgrade(IndexName index, Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		MigrationUpgradeResponse MigrationUpgrade(IMigrationUpgradeRequest request);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		Task<MigrationUpgradeResponse> MigrationUpgradeAsync(IndexName index,
			Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Performs the upgrade of internal indices to make them compatible with the next major version.
		/// Indices must be upgraded one at a time.
		/// </summary>
		Task<MigrationUpgradeResponse> MigrationUpgradeAsync(IMigrationUpgradeRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public MigrationUpgradeResponse MigrationUpgrade(IndexName index, Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null) =>
			MigrationUpgrade(selector.InvokeOrDefault(new MigrationUpgradeDescriptor(index)));

		/// <inheritdoc />
		public MigrationUpgradeResponse MigrationUpgrade(IMigrationUpgradeRequest request) =>
			DoRequest<IMigrationUpgradeRequest, MigrationUpgradeResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<MigrationUpgradeResponse> MigrationUpgradeAsync(
			IndexName index,
			Func<MigrationUpgradeDescriptor, IMigrationUpgradeRequest> selector = null,
			CancellationToken ct = default
		) => MigrationUpgradeAsync(selector.InvokeOrDefault(new MigrationUpgradeDescriptor(index)), ct);

		/// <inheritdoc />
		public Task<MigrationUpgradeResponse> MigrationUpgradeAsync(IMigrationUpgradeRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IMigrationUpgradeRequest, MigrationUpgradeResponse, MigrationUpgradeResponse>
				(request, request.RequestParameters, ct);
	}
}
