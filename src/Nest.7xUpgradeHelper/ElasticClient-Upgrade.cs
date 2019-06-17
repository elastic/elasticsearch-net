using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Upgrade(), please update this usage.")]
		public static UpgradeResponse Upgrade(this IElasticClient client, IUpgradeRequest request)
			=> client.Indices.Upgrade(request);

		[Obsolete("Moved to client.Indices.Upgrade(), please update this usage.")]
		public static UpgradeResponse Upgrade(this IElasticClient client, Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null)
			=> client.Indices.Upgrade(indices, selector);

		[Obsolete("Moved to client.Indices.UpgradeAsync(), please update this usage.")]
		public static Task<UpgradeResponse> UpgradeAsync(this IElasticClient client, IUpgradeRequest request, CancellationToken ct = default)
			=> client.Indices.UpgradeAsync(request, ct);

		[Obsolete("Moved to client.Indices.UpgradeAsync(), please update this usage.")]
		public static Task<UpgradeResponse> UpgradeAsync(this IElasticClient client, Indices indices,
			Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.UpgradeAsync(indices, selector, ct);
	}
}
