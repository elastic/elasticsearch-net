using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes forecasts from a machine learning job.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteForecastResponse DeleteForecast(this IElasticClient client, Id jobId, Ids forecastId,
			Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null
		)
			=> client.MachineLearning.DeleteForecast(jobId, forecastId, selector);

		/// <inheritdoc
		///     cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteForecastResponse DeleteForecast(this IElasticClient client, IDeleteForecastRequest request)
			=> client.MachineLearning.DeleteForecast(request);

		/// <inheritdoc
		///     cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteForecastResponse> DeleteForecastAsync(this IElasticClient client, Id jobId, Ids forecastId,
			Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteForecastAsync(jobId, forecastId, selector, ct);

		/// <inheritdoc
		///     cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteForecastResponse> DeleteForecastAsync(this IElasticClient client, IDeleteForecastRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteForecastAsync(request, ct);
	}
}
