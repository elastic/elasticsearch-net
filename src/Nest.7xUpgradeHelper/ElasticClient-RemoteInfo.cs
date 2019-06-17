using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The cluster remote info API allows to retrieve all of the configured remote cluster information.
		/// <para> </para>
		/// <a href="http://www.elastic.co/guide/en/elasticsearch/reference/master/remote-info.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/remote-info.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the remote info operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RemoteInfoResponse RemoteInfo(this IElasticClient client, Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null)
			=> client.Cluster.RemoteInfo(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static RemoteInfoResponse RemoteInfo(this IElasticClient client, IRemoteInfoRequest request)
			=> client.Cluster.RemoteInfo(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RemoteInfoResponse> RemoteInfoAsync(this IElasticClient client,
			Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.RemoteInfoAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<RemoteInfoResponse> RemoteInfoAsync(this IElasticClient client, IRemoteInfoRequest request, CancellationToken ct = default)
			=> client.Cluster.RemoteInfoAsync(request, ct);
	}
}
