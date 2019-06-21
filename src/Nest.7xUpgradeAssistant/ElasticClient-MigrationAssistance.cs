using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Migration.Assistance(), please update this usage.")]
		public static MigrationAssistanceResponse MigrationAssistance(this IElasticClient client,
			Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null
		)
			=> client.Migration.Assistance(null, selector);

		[Obsolete("Moved to client.Migration.Assistance(), please update this usage.")]
		public static MigrationAssistanceResponse MigrationAssistance(this IElasticClient client, IMigrationAssistanceRequest request)
			=> client.Migration.Assistance(request);

		[Obsolete("Moved to client.Migration.AssistanceAsync(), please update this usage.")]
		public static Task<MigrationAssistanceResponse> MigrationAssistanceAsync(this IElasticClient client,
			Func<MigrationAssistanceDescriptor, IMigrationAssistanceRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Migration.AssistanceAsync(null, selector, ct);

		[Obsolete("Moved to client.Migration.AssistanceAsync(), please update this usage.")]
		public static Task<MigrationAssistanceResponse> MigrationAssistanceAsync(this IElasticClient client, IMigrationAssistanceRequest request,
			CancellationToken ct = default
		)
			=> client.Migration.AssistanceAsync(request, ct);
	}
}
