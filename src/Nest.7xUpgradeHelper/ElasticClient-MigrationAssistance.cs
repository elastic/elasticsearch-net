using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static MigrationAssistanceResponse MigrationAssistance(this IElasticClient client,
			Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null
		)
			=> client.Migration.Assistance(null, selector);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static MigrationAssistanceResponse MigrationAssistance(this IElasticClient client, IMigrationAssistanceRequest request)
			=> client.Migration.Assistance(request);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<MigrationAssistanceResponse> MigrationAssistanceAsync(this IElasticClient client,
			Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Migration.AssistanceAsync(null, selector, ct);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<MigrationAssistanceResponse> MigrationAssistanceAsync(this IElasticClient client, IMigrationAssistanceRequest request,
			CancellationToken ct = default
		)
			=> client.Migration.AssistanceAsync(request, ct);
	}
}
