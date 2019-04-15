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
		ForecastJobResponse ForecastJob(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		ForecastJobResponse ForecastJob(IForecastJobRequest request);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		Task<ForecastJobResponse> ForecastJobAsync(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		Task<ForecastJobResponse> ForecastJobAsync(IForecastJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ForecastJobResponse ForecastJob(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null) =>
			ForecastJob(selector.InvokeOrDefault(new ForecastJobDescriptor(jobId)));

		/// <inheritdoc />
		public ForecastJobResponse ForecastJob(IForecastJobRequest request) =>
			DoRequest<IForecastJobRequest, ForecastJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ForecastJobResponse> ForecastJobAsync(
			Id jobId,
			Func<ForecastJobDescriptor, IForecastJobRequest> selector = null,
			CancellationToken cancellationToken = default
		) => ForecastJobAsync(selector.InvokeOrDefault(new ForecastJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<ForecastJobResponse> ForecastJobAsync(IForecastJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IForecastJobRequest, ForecastJobResponse, ForecastJobResponse>(request, request.RequestParameters, ct);
	}
}
