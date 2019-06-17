using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetJobStats(), please update this usage.")]
		public static GetJobStatsResponse GetJobStats(this IElasticClient client, Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null)
			=> client.MachineLearning.GetJobStats(selector);

		[Obsolete("Moved to client.MachineLearning.GetJobStats(), please update this usage.")]
		public static GetJobStatsResponse GetJobStats(this IElasticClient client, IGetJobStatsRequest request)
			=> client.MachineLearning.GetJobStats(request);

		[Obsolete("Moved to client.MachineLearning.GetJobStatsAsync(), please update this usage.")]
		public static Task<GetJobStatsResponse> GetJobStatsAsync(this IElasticClient client,
			Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetJobStatsAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetJobStatsAsync(), please update this usage.")]
		public static Task<GetJobStatsResponse> GetJobStatsAsync(this IElasticClient client, IGetJobStatsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetJobStatsAsync(request, ct);
	}
}
