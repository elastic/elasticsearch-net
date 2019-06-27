using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.StopDatafeed(), please update this usage.")]
		public static StopDatafeedResponse StopDatafeed(this IElasticClient client, Id datafeedId,
			Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null
		)
			=> client.MachineLearning.StopDatafeed(datafeedId, selector);

		[Obsolete("Moved to client.MachineLearning.StopDatafeed(), please update this usage.")]
		public static StopDatafeedResponse StopDatafeed(this IElasticClient client, IStopDatafeedRequest request)
			=> client.MachineLearning.StopDatafeed(request);

		[Obsolete("Moved to client.MachineLearning.StopDatafeedAsync(), please update this usage.")]
		public static Task<StopDatafeedResponse> StopDatafeedAsync(this IElasticClient client, Id datafeedId,
			Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.StopDatafeedAsync(datafeedId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.StopDatafeedAsync(), please update this usage.")]
		public static Task<StopDatafeedResponse> StopDatafeedAsync(this IElasticClient client, IStopDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.StopDatafeedAsync(request, ct);
	}
}
