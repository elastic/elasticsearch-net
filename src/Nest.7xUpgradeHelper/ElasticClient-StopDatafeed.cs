using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Stop a machine learning data feed.
		/// A datafeed that is stopped ceases to retrieve data from Elasticsearch. A datafeed can be started and stopped multiple
		/// times throughout its
		/// lifecycle.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StopDatafeedResponse StopDatafeed(this IElasticClient client, Id datafeedId,
			Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null
		)
			=> client.MachineLearning.StopDatafeed(datafeedId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StopDatafeedResponse StopDatafeed(this IElasticClient client, IStopDatafeedRequest request)
			=> client.MachineLearning.StopDatafeed(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StopDatafeedResponse> StopDatafeedAsync(this IElasticClient client, Id datafeedId,
			Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.StopDatafeedAsync(datafeedId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StopDatafeedResponse> StopDatafeedAsync(this IElasticClient client, IStopDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.StopDatafeedAsync(request, ct);
	}
}
