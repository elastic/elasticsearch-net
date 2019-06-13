using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static ClusterPutSettingsResponse ClusterPutSettings(this IElasticClient client,Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector);

		/// <inheritdoc />
		public static Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(this IElasticClient client,Func<ClusterPutSettingsDescriptor, IClusterPutSettingsRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static ClusterPutSettingsResponse ClusterPutSettings(this IElasticClient client,IClusterPutSettingsRequest request);

		/// <inheritdoc />
		public static Task<ClusterPutSettingsResponse> ClusterPutSettingsAsync(this IElasticClient client,IClusterPutSettingsRequest request,
			CancellationToken ct = default
		);
	}

}
