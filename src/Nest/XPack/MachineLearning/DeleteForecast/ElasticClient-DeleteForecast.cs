using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes forecasts from a machine learning job.
		/// </summary>
		IDeleteForecastResponse DeleteForecast(Id jobId, ForecastIds forecastId, Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null);

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		IDeleteForecastResponse DeleteForecast(IDeleteForecastRequest request);

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		Task<IDeleteForecastResponse> DeleteForecastAsync(Id jobId, ForecastIds forecastId, Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		Task<IDeleteForecastResponse> DeleteForecastAsync(IDeleteForecastRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		public IDeleteForecastResponse DeleteForecast(Id jobId, ForecastIds forecastId, Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null) =>
			DeleteForecast(selector.InvokeOrDefault(new DeleteForecastDescriptor(jobId, forecastId)));

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		public IDeleteForecastResponse DeleteForecast(IDeleteForecastRequest request) =>
			DoRequest<IDeleteForecastRequest, DeleteForecastResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		public Task<IDeleteForecastResponse> DeleteForecastAsync(
			Id jobId,
			ForecastIds forecastId,
			Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null,
			CancellationToken ct = default
		) => DeleteForecastAsync(selector.InvokeOrDefault(new DeleteForecastDescriptor(jobId, forecastId)), ct);

		/// <inheritdoc cref="DeleteForecast(Nest.Id,Nest.ForecastIds,System.Func{Nest.DeleteForecastDescriptor,Nest.IDeleteForecastRequest})" />
		public Task<IDeleteForecastResponse> DeleteForecastAsync(IDeleteForecastRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteForecastRequest, IDeleteForecastResponse, DeleteForecastResponse>(request, request.RequestParameters, ct);
	}
}
