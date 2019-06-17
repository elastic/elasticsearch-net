using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ForecastJobResponse ForecastJob(this IElasticClient client, Id jobId,
			Func<ForecastJobDescriptor, IForecastJobRequest> selector = null
		)
			=> client.MachineLearning.ForecastJob(jobId, selector);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ForecastJobResponse ForecastJob(this IElasticClient client, IForecastJobRequest request)
			=> client.MachineLearning.ForecastJob(request);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ForecastJobResponse> ForecastJobAsync(this IElasticClient client, Id jobId,
			Func<ForecastJobDescriptor, IForecastJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.ForecastJobAsync(jobId, selector, ct);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ForecastJobResponse> ForecastJobAsync(this IElasticClient client, IForecastJobRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.ForecastJobAsync(request, ct);
	}
}
