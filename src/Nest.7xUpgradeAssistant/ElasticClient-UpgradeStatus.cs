using System;
using System.Threading;
using System.Threading.Tasks;
using static Nest.Infer;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.UpgradeStatus(), please update this usage.")]
		public static UpgradeStatusResponse UpgradeStatus(this IElasticClient client, IUpgradeStatusRequest request)
			=> client.Indices.UpgradeStatus(request);

		[Obsolete("Moved to client.Indices.UpgradeStatus(), please update this usage.")]
		public static UpgradeStatusResponse UpgradeStatus(this IElasticClient client,
			Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null
		)
			=> client.Indices.UpgradeStatus(AllIndices, selector);

		[Obsolete("Moved to client.Indices.UpgradeStatusAsync(), please update this usage.")]
		public static Task<UpgradeStatusResponse> UpgradeStatusAsync(this IElasticClient client, IUpgradeStatusRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.UpgradeStatusAsync(request, ct);

		[Obsolete("Moved to client.Indices.UpgradeStatusAsync(), please update this usage.")]
		public static Task<UpgradeStatusResponse> UpgradeStatusAsync(this IElasticClient client,
			Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.UpgradeStatusAsync(AllIndices, selector, ct);
	}
}
