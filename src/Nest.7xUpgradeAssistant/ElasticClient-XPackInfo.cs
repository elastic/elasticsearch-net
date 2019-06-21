using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.XPack.Info(), please update this usage.")]
		public static XPackInfoResponse XPackInfo(this IElasticClient client, Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null)
			=> client.XPack.Info(selector);

		[Obsolete("Moved to client.XPack.Info(), please update this usage.")]
		public static XPackInfoResponse XPackInfo(this IElasticClient client, IXPackInfoRequest request)
			=> client.XPack.Info(request);

		[Obsolete("Moved to client.XPack.InfoAsync(), please update this usage.")]
		public static Task<XPackInfoResponse> XPackInfoAsync(this IElasticClient client, Func<XPackInfoDescriptor, IXPackInfoRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.XPack.InfoAsync(selector, ct);

		[Obsolete("Moved to client.XPack.InfoAsync(), please update this usage.")]
		public static Task<XPackInfoResponse> XPackInfoAsync(this IElasticClient client, IXPackInfoRequest request, CancellationToken ct = default)
			=> client.XPack.InfoAsync(request, ct);
	}
}
