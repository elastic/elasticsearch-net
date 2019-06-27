using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Migration.DeprecationInfo(), please update this usage.")]
		public static DeprecationInfoResponse DeprecationInfo(this IElasticClient client,
			Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null
		)
			=> client.Migration.DeprecationInfo(selector);

		[Obsolete("Moved to client.Migration.DeprecationInfo(), please update this usage.")]
		public static DeprecationInfoResponse DeprecationInfo(this IElasticClient client, IDeprecationInfoRequest request)
			=> client.Migration.DeprecationInfo(request);

		[Obsolete("Moved to client.Migration.DeprecationInfoAsync(), please update this usage.")]
		public static Task<DeprecationInfoResponse> DeprecationInfoAsync(this IElasticClient client,
			Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Migration.DeprecationInfoAsync(selector, ct);

		[Obsolete("Moved to client.Migration.DeprecationInfoAsync(), please update this usage.")]
		public static Task<DeprecationInfoResponse> DeprecationInfoAsync(this IElasticClient client, IDeprecationInfoRequest request,
			CancellationToken ct = default
		)
			=> client.Migration.DeprecationInfoAsync(request, ct);
	}
}
