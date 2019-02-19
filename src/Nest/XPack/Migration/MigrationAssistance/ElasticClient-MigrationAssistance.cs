using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		IMigrationAssistanceResponse MigrationAssistance(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		IMigrationAssistanceResponse MigrationAssistance(IMigrationAssistanceRequest request);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		Task<IMigrationAssistanceResponse> MigrationAssistanceAsync(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		Task<IMigrationAssistanceResponse> MigrationAssistanceAsync(IMigrationAssistanceRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMigrationAssistanceResponse MigrationAssistance(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null) =>
			MigrationAssistance(selector.InvokeOrDefault(new MigrationAssistanceDescriptor()));

		/// <inheritdoc />
		public IMigrationAssistanceResponse MigrationAssistance(IMigrationAssistanceRequest request) =>
			Dispatcher.Dispatch<IMigrationAssistanceRequest, MigrationAssistanceRequestParameters, MigrationAssistanceResponse>(
				request,
				(p, d) => LowLevelDispatch.MigrationGetAssistanceDispatch<MigrationAssistanceResponse>(p)
			);

		/// <inheritdoc />
		public Task<IMigrationAssistanceResponse> MigrationAssistanceAsync(
			Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			MigrationAssistanceAsync(selector.InvokeOrDefault(new MigrationAssistanceDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IMigrationAssistanceResponse> MigrationAssistanceAsync(IMigrationAssistanceRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IMigrationAssistanceRequest, MigrationAssistanceRequestParameters, MigrationAssistanceResponse,
					IMigrationAssistanceResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.MigrationGetAssistanceDispatchAsync<MigrationAssistanceResponse>(p, c)
				);
	}
}
