using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves a watch by its id
		/// </summary>
		public static GetWatchResponse GetWatch(this IElasticClient client,Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null);

		/// <inheritdoc />
		public static GetWatchResponse GetWatch(this IElasticClient client,IGetWatchRequest request);

		/// <inheritdoc />
		public static Task<GetWatchResponse> GetWatchAsync(this IElasticClient client,Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetWatchResponse> GetWatchAsync(this IElasticClient client,IGetWatchRequest request, CancellationToken ct = default);
	}

}
