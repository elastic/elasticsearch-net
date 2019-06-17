using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeprecationInfoResponse DeprecationInfo(this IElasticClient client,
			Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null
		)
			=> client.Migration.DeprecationInfo(selector);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeprecationInfoResponse DeprecationInfo(this IElasticClient client, IDeprecationInfoRequest request)
			=> client.Migration.DeprecationInfo(request);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeprecationInfoResponse> DeprecationInfoAsync(this IElasticClient client,
			Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Migration.DeprecationInfoAsync(selector, ct);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeprecationInfoResponse> DeprecationInfoAsync(this IElasticClient client, IDeprecationInfoRequest request,
			CancellationToken ct = default
		)
			=> client.Migration.DeprecationInfoAsync(request, ct);
	}
}
