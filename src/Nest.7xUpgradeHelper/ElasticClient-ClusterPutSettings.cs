using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.PutSettings(), please update this usage.")]
		public static ClusterPutSettingsResponse ClusterPutSettings(this IElasticClient client,
			Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector
		)
			=> client.Cluster.PutSettings(selector);

		[Obsolete("Moved to client.Cluster.PutSettingsAsync(), please update this usage.")]
		public static Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(this IElasticClient client,
			Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector,
			CancellationToken ct = default
		)
			=> client.Cluster.PutSettingsAsync(selector, ct);

		[Obsolete("Moved to client.Cluster.PutSettings(), please update this usage.")]
		public static ClusterPutSettingsResponse ClusterPutSettings(this IElasticClient client, IClusterPutSettingsRequest request)
			=> client.Cluster.PutSettings(request);

		[Obsolete("Moved to client.Cluster.PutSettingsAsync(), please update this usage.")]
		public static Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(this IElasticClient client, IClusterPutSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.PutSettingsAsync(request, ct);
	}
}
