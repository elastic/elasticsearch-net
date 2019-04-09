using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		IForecastJobResponse ForecastJob(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		IForecastJobResponse ForecastJob(IForecastJobRequest request);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		Task<IForecastJobResponse> ForecastJobAsync(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		Task<IForecastJobResponse> ForecastJobAsync(IForecastJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IForecastJobResponse ForecastJob(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null) =>
			ForecastJob(selector.InvokeOrDefault(new ForecastJobDescriptor(jobId)));

		/// <inheritdoc />
		public IForecastJobResponse ForecastJob(IForecastJobRequest request) =>
			Dispatch2<IForecastJobRequest, ForecastJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IForecastJobResponse> ForecastJobAsync(
			Id jobId,
			Func<ForecastJobDescriptor, IForecastJobRequest> selector = null,
			CancellationToken cancellationToken = default
		) => ForecastJobAsync(selector.InvokeOrDefault(new ForecastJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IForecastJobResponse> ForecastJobAsync(IForecastJobRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IForecastJobRequest, IForecastJobResponse, ForecastJobResponse>(request, request.RequestParameters, ct);
	}
}
