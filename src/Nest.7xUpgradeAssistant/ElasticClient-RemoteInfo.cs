using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.RemoteInfo(), please update this usage.")]
		public static RemoteInfoResponse RemoteInfo(this IElasticClient client, Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null)
			=> client.Cluster.RemoteInfo(selector);

		[Obsolete("Moved to client.Cluster.RemoteInfo(), please update this usage.")]
		public static RemoteInfoResponse RemoteInfo(this IElasticClient client, IRemoteInfoRequest request)
			=> client.Cluster.RemoteInfo(request);

		[Obsolete("Moved to client.Cluster.RemoteInfoAsync(), please update this usage.")]
		public static Task<RemoteInfoResponse> RemoteInfoAsync(this IElasticClient client,
			Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.RemoteInfoAsync(selector, ct);

		[Obsolete("Moved to client.Cluster.RemoteInfoAsync(), please update this usage.")]
		public static Task<RemoteInfoResponse> RemoteInfoAsync(this IElasticClient client, IRemoteInfoRequest request, CancellationToken ct = default)
			=> client.Cluster.RemoteInfoAsync(request, ct);
	}
}
