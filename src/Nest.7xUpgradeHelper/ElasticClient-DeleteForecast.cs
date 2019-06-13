using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes forecasts from a machine learning job.
		/// </summary>
		public static DeleteForecastResponse DeleteForecast(this IElasticClient client,Id jobId, ForecastIds forecastId, Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null);

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		public static DeleteForecastResponse DeleteForecast(this IElasticClient client,IDeleteForecastRequest request);

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		public static Task<DeleteForecastResponse> DeleteForecastAsync(this IElasticClient client,Id jobId, ForecastIds forecastId, Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		public static Task<DeleteForecastResponse> DeleteForecastAsync(this IElasticClient client,IDeleteForecastRequest request,
			CancellationToken ct = default
		);
	}

}
