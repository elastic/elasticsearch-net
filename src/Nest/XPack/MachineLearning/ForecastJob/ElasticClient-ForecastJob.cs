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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <summary>
		/// Uses historical behavior to predict the future behavior of a time series.
		/// </summary>
		Task<IForecastJobResponse> ForecastJobAsync(IForecastJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IForecastJobResponse ForecastJob(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null) =>
			ForecastJob(selector.InvokeOrDefault(new ForecastJobDescriptor(jobId)));

		/// <inheritdoc />
		public IForecastJobResponse ForecastJob(IForecastJobRequest request) =>
			Dispatcher.Dispatch<IForecastJobRequest, ForecastJobRequestParameters, ForecastJobResponse>(
				request,
				(p, d) => LowLevelDispatch.MlForecastDispatch<ForecastJobResponse>(p)
			);

		/// <inheritdoc />
		public Task<IForecastJobResponse> ForecastJobAsync(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ForecastJobAsync(selector.InvokeOrDefault(new ForecastJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IForecastJobResponse> ForecastJobAsync(IForecastJobRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IForecastJobRequest, ForecastJobRequestParameters, ForecastJobResponse, IForecastJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.MlForecastDispatchAsync<ForecastJobResponse>(p, c)
			);
	}
}
