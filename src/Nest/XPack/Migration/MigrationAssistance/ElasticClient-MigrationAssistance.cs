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
		MigrationAssistanceResponse MigrationAssistance(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		MigrationAssistanceResponse MigrationAssistance(IMigrationAssistanceRequest request);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		Task<MigrationAssistanceResponse> MigrationAssistanceAsync(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		Task<MigrationAssistanceResponse> MigrationAssistanceAsync(IMigrationAssistanceRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public MigrationAssistanceResponse MigrationAssistance(Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null) =>
			MigrationAssistance(selector.InvokeOrDefault(new MigrationAssistanceDescriptor()));

		/// <inheritdoc />
		public MigrationAssistanceResponse MigrationAssistance(IMigrationAssistanceRequest request) =>
			DoRequest<IMigrationAssistanceRequest, MigrationAssistanceResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<MigrationAssistanceResponse> MigrationAssistanceAsync(
			Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null,
			CancellationToken ct = default
		) => MigrationAssistanceAsync(selector.InvokeOrDefault(new MigrationAssistanceDescriptor()), ct);

		/// <inheritdoc />
		public Task<MigrationAssistanceResponse> MigrationAssistanceAsync(IMigrationAssistanceRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IMigrationAssistanceRequest, MigrationAssistanceResponse, MigrationAssistanceResponse>
				(request, request.RequestParameters, ct);
	}
}
