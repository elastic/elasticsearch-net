using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.GetSettings(), please update this usage.")]
		public static ClusterGetSettingsResponse ClusterGetSettings(this IElasticClient client,
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null
		)
			=> client.Cluster.GetSettings(selector);

		[Obsolete("Moved to client.Cluster.GetSettingsAsync(), please update this usage.")]
		public static Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(this IElasticClient client,
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.GetSettingsAsync(selector, ct);

		[Obsolete("Moved to client.Cluster.GetSettings(), please update this usage.")]
		public static ClusterGetSettingsResponse ClusterGetSettings(this IElasticClient client, IClusterGetSettingsRequest request)
			=> client.Cluster.GetSettings(request);

		[Obsolete("Moved to client.Cluster.GetSettingsAsync(), please update this usage.")]
		public static Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(this IElasticClient client, IClusterGetSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.GetSettingsAsync(request, ct);
	}
}
