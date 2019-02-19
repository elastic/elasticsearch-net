using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Forces any buffered data to be processed by the machine learning job.
		/// </summary>
		IFlushJobResponse FlushJob(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null);

		/// <inheritdoc />
		IFlushJobResponse FlushJob(IFlushJobRequest request);

		/// <inheritdoc />
		Task<IFlushJobResponse> FlushJobAsync(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IFlushJobResponse> FlushJobAsync(IFlushJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IFlushJobResponse FlushJob(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null) =>
			FlushJob(selector.InvokeOrDefault(new FlushJobDescriptor(jobId)));

		/// <inheritdoc />
		public IFlushJobResponse FlushJob(IFlushJobRequest request) =>
			Dispatcher.Dispatch<IFlushJobRequest, FlushJobRequestParameters, FlushJobResponse>(
				request,
				LowLevelDispatch.MlFlushJobDispatch<FlushJobResponse>
			);

		/// <inheritdoc />
		public Task<IFlushJobResponse> FlushJobAsync(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			FlushJobAsync(selector.InvokeOrDefault(new FlushJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IFlushJobResponse> FlushJobAsync(IFlushJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IFlushJobRequest, FlushJobRequestParameters, FlushJobResponse, IFlushJobResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.MlFlushJobDispatchAsync<FlushJobResponse>
			);
	}
}
