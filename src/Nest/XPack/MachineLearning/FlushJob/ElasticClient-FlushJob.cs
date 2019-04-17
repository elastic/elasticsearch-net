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
		FlushJobResponse FlushJob(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null);

		/// <inheritdoc />
		FlushJobResponse FlushJob(IFlushJobRequest request);

		/// <inheritdoc />
		Task<FlushJobResponse> FlushJobAsync(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<FlushJobResponse> FlushJobAsync(IFlushJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public FlushJobResponse FlushJob(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null) =>
			FlushJob(selector.InvokeOrDefault(new FlushJobDescriptor(jobId)));

		/// <inheritdoc />
		public FlushJobResponse FlushJob(IFlushJobRequest request) =>
			DoRequest<IFlushJobRequest, FlushJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<FlushJobResponse> FlushJobAsync(
			Id jobId,
			Func<FlushJobDescriptor, IFlushJobRequest> selector = null,
			CancellationToken cancellationToken = default
		) => FlushJobAsync(selector.InvokeOrDefault(new FlushJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<FlushJobResponse> FlushJobAsync(IFlushJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IFlushJobRequest, FlushJobResponse>(request, request.RequestParameters, ct);
	}
}
