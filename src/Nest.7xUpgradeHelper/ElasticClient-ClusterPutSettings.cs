using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Allows to update cluster wide specific settings. Settings updated can either be persistent
		/// (applied cross restarts) or transient (will not survive a full cluster restart).
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterPutSettingsResponse ClusterPutSettings(this IElasticClient client,
			Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector
		)
			=> client.Cluster.PutSettings(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(this IElasticClient client,
			Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector,
			CancellationToken ct = default
		)
			=> client.Cluster.PutSettingsAsync(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterPutSettingsResponse ClusterPutSettings(this IElasticClient client, IClusterPutSettingsRequest request)
			=> client.Cluster.PutSettings(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(this IElasticClient client, IClusterPutSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.PutSettingsAsync(request, ct);
	}
}
