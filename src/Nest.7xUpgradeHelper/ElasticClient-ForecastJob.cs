using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.ForecastJob(), please update this usage.")]
		public static ForecastJobResponse ForecastJob(this IElasticClient client, Id jobId,
			Func<ForecastJobDescriptor, IForecastJobRequest> selector = null
		)
			=> client.MachineLearning.ForecastJob(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.ForecastJob(), please update this usage.")]
		public static ForecastJobResponse ForecastJob(this IElasticClient client, IForecastJobRequest request)
			=> client.MachineLearning.ForecastJob(request);

		[Obsolete("Moved to client.MachineLearning.ForecastJobAsync(), please update this usage.")]
		public static Task<ForecastJobResponse> ForecastJobAsync(this IElasticClient client, Id jobId,
			Func<ForecastJobDescriptor, IForecastJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.ForecastJobAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.ForecastJobAsync(), please update this usage.")]
		public static Task<ForecastJobResponse> ForecastJobAsync(this IElasticClient client, IForecastJobRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.ForecastJobAsync(request, ct);
	}
}
