using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.StartDatafeed(), please update this usage.")]
		public static StartDatafeedResponse StartDatafeed(this IElasticClient client, Id datafeedId,
			Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null
		)
			=> client.MachineLearning.StartDatafeed(datafeedId, selector);

		[Obsolete("Moved to client.MachineLearning.StartDatafeed(), please update this usage.")]
		public static StartDatafeedResponse StartDatafeed(this IElasticClient client, IStartDatafeedRequest request)
			=> client.MachineLearning.StartDatafeed(request);

		[Obsolete("Moved to client.MachineLearning.StartDatafeedAsync(), please update this usage.")]
		public static Task<StartDatafeedResponse> StartDatafeedAsync(this IElasticClient client, Id datafeedId,
			Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.StartDatafeedAsync(datafeedId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.StartDatafeedAsync(), please update this usage.")]
		public static Task<StartDatafeedResponse> StartDatafeedAsync(this IElasticClient client, IStartDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.StartDatafeedAsync(request, ct);
	}
}
