using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.DeleteForecast(), please update this usage.")]
		public static DeleteForecastResponse DeleteForecast(this IElasticClient client, Id jobId, Ids forecastId,
			Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null
		)
			=> client.MachineLearning.DeleteForecast(jobId, forecastId, selector);

		[Obsolete("Moved to client.MachineLearning.DeleteForecast(), please update this usage.")]
		public static DeleteForecastResponse DeleteForecast(this IElasticClient client, IDeleteForecastRequest request)
			=> client.MachineLearning.DeleteForecast(request);

		[Obsolete("Moved to client.MachineLearning.DeleteForecastAsync(), please update this usage.")]
		public static Task<DeleteForecastResponse> DeleteForecastAsync(this IElasticClient client, Id jobId, Ids forecastId,
			Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteForecastAsync(jobId, forecastId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.DeleteForecastAsync(), please update this usage.")]
		public static Task<DeleteForecastResponse> DeleteForecastAsync(this IElasticClient client, IDeleteForecastRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteForecastAsync(request, ct);
	}
}
