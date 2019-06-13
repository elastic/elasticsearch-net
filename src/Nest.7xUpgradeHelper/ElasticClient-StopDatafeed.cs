using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Stop a machine learning data feed.
		/// A datafeed that is stopped ceases to retrieve data from Elasticsearch. A datafeed can be started and stopped multiple times throughout its
		/// lifecycle.
		/// </summary>
		public static StopDatafeedResponse StopDatafeed(this IElasticClient client,Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null);

		/// <inheritdoc />
		public static StopDatafeedResponse StopDatafeed(this IElasticClient client,IStopDatafeedRequest request);

		/// <inheritdoc />
		public static Task<StopDatafeedResponse> StopDatafeedAsync(this IElasticClient client,Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<StopDatafeedResponse> StopDatafeedAsync(this IElasticClient client,IStopDatafeedRequest request, CancellationToken ct = default);
	}

}
