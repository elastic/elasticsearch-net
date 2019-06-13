using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using PingConverter = Func<IApiCallDetails, Stream, PingResponse>;

	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Executes a HEAD request to the cluster to determine whether it's up or not.
		/// </summary>
		public static PingResponse Ping(this IElasticClient client,Func<PingDescriptor, IPingRequest> selector = null);

		/// <inheritdoc />
		public static Task<PingResponse> PingAsync(this IElasticClient client,Func<PingDescriptor, IPingRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static PingResponse Ping(this IElasticClient client,IPingRequest request);

		/// <inheritdoc />
		public static Task<PingResponse> PingAsync(this IElasticClient client,IPingRequest request, CancellationToken ct = default);
	}

}
