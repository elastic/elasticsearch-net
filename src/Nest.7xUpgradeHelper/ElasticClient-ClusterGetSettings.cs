using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static ClusterGetSettingsResponse ClusterGetSettings(this IElasticClient client,Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public static Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(this IElasticClient client,
			Func<ClusterGetSettingsDescriptor, IClusterGetSettingsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public static ClusterGetSettingsResponse ClusterGetSettings(this IElasticClient client,IClusterGetSettingsRequest request);

		/// <inheritdoc cref="ClusterGetSettings(System.Func{Nest.ClusterGetSettingsDescriptor,Nest.IClusterGetSettingsRequest})"/>
		public static Task<ClusterGetSettingsResponse> ClusterGetSettingsAsync(this IElasticClient client,IClusterGetSettingsRequest request, CancellationToken ct = default);
	}

}
