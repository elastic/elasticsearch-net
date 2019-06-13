using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		public static MigrationAssistanceResponse MigrationAssistance(this IElasticClient client,Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		public static MigrationAssistanceResponse MigrationAssistance(this IElasticClient client,IMigrationAssistanceRequest request);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		public static Task<MigrationAssistanceResponse> MigrationAssistanceAsync(this IElasticClient client,Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Analyzes existing indices in the cluster and returns the information about indices that
		/// require some changes before the cluster can be upgraded to the next major version.
		/// </summary>
		public static Task<MigrationAssistanceResponse> MigrationAssistanceAsync(this IElasticClient client,IMigrationAssistanceRequest request,
			CancellationToken ct = default
		);
	}

}
