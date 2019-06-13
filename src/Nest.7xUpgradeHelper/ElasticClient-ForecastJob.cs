using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		public static ForecastJobResponse ForecastJob(this IElasticClient client,Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		public static ForecastJobResponse ForecastJob(this IElasticClient client,IForecastJobRequest request);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		public static Task<ForecastJobResponse> ForecastJobAsync(this IElasticClient client,Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		public static Task<ForecastJobResponse> ForecastJobAsync(this IElasticClient client,IForecastJobRequest request, CancellationToken ct = default);
	}

}
