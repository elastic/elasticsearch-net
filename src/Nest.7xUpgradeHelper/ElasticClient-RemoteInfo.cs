using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static RemoteInfoResponse RemoteInfo(this IElasticClient client,Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null);

		/// <inheritdoc />
		public static RemoteInfoResponse RemoteInfo(this IElasticClient client,IRemoteInfoRequest request);

		/// <inheritdoc />
		public static Task<RemoteInfoResponse> RemoteInfoAsync(this IElasticClient client,Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<RemoteInfoResponse> RemoteInfoAsync(this IElasticClient client,IRemoteInfoRequest request, CancellationToken ct = default);
	}

}
