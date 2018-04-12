using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

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
		Task<IMigrationAssistanceResponse> MigrationAssistanceAsync(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		Task<IMigrationAssistanceResponse> MigrationAssistanceAsync(IMigrationAssistanceRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMigrationAssistanceResponse MigrationAssistance(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null) =>
			this.MigrationAssistance(selector.InvokeOrDefault(new MigrationAssistanceDescriptor()));

		/// <inheritdoc/>
		public IMigrationAssistanceResponse MigrationAssistance(IMigrationAssistanceRequest request) =>
			this.Dispatcher.Dispatch<IMigrationAssistanceRequest, MigrationAssistanceRequestParameters, MigrationAssistanceResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackMigrationGetAssistanceDispatch<MigrationAssistanceResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IMigrationAssistanceResponse> MigrationAssistanceAsync(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.MigrationAssistanceAsync(selector.InvokeOrDefault(new MigrationAssistanceDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IMigrationAssistanceResponse> MigrationAssistanceAsync(IMigrationAssistanceRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IMigrationAssistanceRequest, MigrationAssistanceRequestParameters, MigrationAssistanceResponse, IMigrationAssistanceResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMigrationGetAssistanceDispatchAsync<MigrationAssistanceResponse>(p, c)
			);
	}
}
