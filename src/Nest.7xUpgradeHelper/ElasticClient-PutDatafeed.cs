using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.PutDatafeed(), please update this usage.")]
		public static PutDatafeedResponse PutDatafeed<T>(this IElasticClient client, Id datafeedId,
			Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null
		) where T : class
			=> client.MachineLearning.PutDatafeed(datafeedId, selector);

		[Obsolete("Moved to client.MachineLearning.PutDatafeed(), please update this usage.")]
		public static PutDatafeedResponse PutDatafeed(this IElasticClient client, IPutDatafeedRequest request)
			=> client.MachineLearning.PutDatafeed(request);

		[Obsolete("Moved to client.MachineLearning.PutDatafeedAsync(), please update this usage.")]
		public static Task<PutDatafeedResponse> PutDatafeedAsync<T>(this IElasticClient client, Id datafeedId,
			Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.PutDatafeedAsync(datafeedId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.PutDatafeedAsync(), please update this usage.")]
		public static Task<PutDatafeedResponse> PutDatafeedAsync(this IElasticClient client, IPutDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutDatafeedAsync(request, ct);
	}
}
