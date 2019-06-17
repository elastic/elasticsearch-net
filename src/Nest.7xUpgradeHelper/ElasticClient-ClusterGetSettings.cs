using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets cluster wide specific settings. Settings updated can either be persistent
		/// (applied cross restarts) or transient (will not survive a full cluster restart).
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterGetSettingsResponse ClusterGetSettings(this IElasticClient client,
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null
		)
			=> client.Cluster.GetSettings(selector);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(this IElasticClient client,
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.GetSettingsAsync(selector, ct);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterGetSettingsResponse ClusterGetSettings(this IElasticClient client, IClusterGetSettingsRequest request)
			=> client.Cluster.GetSettings(request);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(this IElasticClient client, IClusterGetSettingsRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.GetSettingsAsync(request, ct);
	}
}
