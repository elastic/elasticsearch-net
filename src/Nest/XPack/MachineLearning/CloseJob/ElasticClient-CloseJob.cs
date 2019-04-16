using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Closes a machine learning job.
		/// A closed job cannot receive data or perform analysis operations, but you can still explore and navigate results.
		/// </summary>
		ICloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null);

		/// <inheritdoc />
		ICloseJobResponse CloseJob(ICloseJobRequest request);

		/// <inheritdoc />
		Task<ICloseJobResponse> CloseJobAsync(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null) =>
			CloseJob(selector.InvokeOrDefault(new CloseJobDescriptor(jobId)));

		/// <inheritdoc />
		public ICloseJobResponse CloseJob(ICloseJobRequest request) =>
			Dispatcher.Dispatch<ICloseJobRequest, CloseJobRequestParameters, CloseJobResponse>(
				request,
				(p, d) => LowLevelDispatch.MlCloseJobDispatch<CloseJobResponse>(p, d)
			);

		/// <inheritdoc />
		public Task<ICloseJobResponse> CloseJobAsync(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CloseJobAsync(selector.InvokeOrDefault(new CloseJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<ICloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<ICloseJobRequest, CloseJobRequestParameters, CloseJobResponse, ICloseJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.MlCloseJobDispatchAsync<CloseJobResponse>(p, d, c)
			);
	}
}
