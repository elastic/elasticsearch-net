using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.UpdateDatafeed(), please update this usage.")]
		public static UpdateDatafeedResponse UpdateDatafeed<T>(this IElasticClient client, Id datafeedId,
			Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null
		)
			where T : class
			=> client.MachineLearning.UpdateDatafeed(datafeedId, selector);

		[Obsolete("Moved to client.MachineLearning.UpdateDatafeed(), please update this usage.")]
		public static UpdateDatafeedResponse UpdateDatafeed(this IElasticClient client, IUpdateDatafeedRequest request)
			=> client.MachineLearning.UpdateDatafeed(request);

		[Obsolete("Moved to client.MachineLearning.UpdateDatafeedAsync(), please update this usage.")]
		public static Task<UpdateDatafeedResponse> UpdateDatafeedAsync<T>(this IElasticClient client, Id datafeedId,
			Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.UpdateDatafeedAsync(datafeedId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.UpdateDatafeedAsync(), please update this usage.")]
		public static Task<UpdateDatafeedResponse> UpdateDatafeedAsync(this IElasticClient client, IUpdateDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.UpdateDatafeedAsync(request, ct);
	}
}
