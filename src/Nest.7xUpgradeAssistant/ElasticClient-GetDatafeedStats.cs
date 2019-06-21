using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetDatafeedStats(), please update this usage.")]
		public static GetDatafeedStatsResponse GetDatafeedStats(this IElasticClient client,
			Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null
		)
			=> client.MachineLearning.GetDatafeedStats(selector);

		[Obsolete("Moved to client.MachineLearning.GetDatafeedStats(), please update this usage.")]
		public static GetDatafeedStatsResponse GetDatafeedStats(this IElasticClient client, IGetDatafeedStatsRequest request)
			=> client.MachineLearning.GetDatafeedStats(request);

		[Obsolete("Moved to client.MachineLearning.GetDatafeedStatsAsync(), please update this usage.")]
		public static Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(this IElasticClient client,
			Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetDatafeedStatsAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetDatafeedStatsAsync(), please update this usage.")]
		public static Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(this IElasticClient client, IGetDatafeedStatsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetDatafeedStatsAsync(request, ct);
	}
}
