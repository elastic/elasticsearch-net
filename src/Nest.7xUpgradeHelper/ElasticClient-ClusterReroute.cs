using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Allows to explicitly execute a cluster reroute allocation command including specific commands.
		/// For example, a shard can be moved from one node to another explicitly, an allocation can be canceled,
		/// or an unassigned shard can be explicitly allocated on a specific node.
		/// </summary>
		public static ClusterRerouteResponse ClusterReroute(this IElasticClient client,Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector);

		/// <inheritdoc />
		public static Task<ClusterRerouteResponse> ClusterRerouteAsync(this IElasticClient client,Func<ClusterRerouteDescriptor, IClusterRerouteRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static ClusterRerouteResponse ClusterReroute(this IElasticClient client,IClusterRerouteRequest request);

		/// <inheritdoc />
		public static Task<ClusterRerouteResponse> ClusterRerouteAsync(this IElasticClient client,IClusterRerouteRequest request,
			CancellationToken ct = default
		);
	}

}
