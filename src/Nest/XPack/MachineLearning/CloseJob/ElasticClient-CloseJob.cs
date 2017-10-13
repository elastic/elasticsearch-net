using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Closes a Machine Learning job.
		/// A closed job cannot receive data or perform analysis operations, but you can still explore and navigate results.
		/// </summary>
		ICloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null);

		/// <inheritdoc/>
		ICloseJobResponse CloseJob(ICloseJobRequest request);

		/// <inheritdoc/>
		Task<ICloseJobResponse> CloseJobAsync(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ICloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null) =>
			this.CloseJob(selector.InvokeOrDefault(new CloseJobDescriptor(jobId)));

		/// <inheritdoc/>
		public ICloseJobResponse CloseJob(ICloseJobRequest request) =>
			this.Dispatcher.Dispatch<ICloseJobRequest, CloseJobRequestParameters, CloseJobResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlCloseJobDispatch<CloseJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ICloseJobResponse> CloseJobAsync(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.CloseJobAsync(selector.InvokeOrDefault(new CloseJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<ICloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<ICloseJobRequest, CloseJobRequestParameters, CloseJobResponse, ICloseJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlCloseJobDispatchAsync<CloseJobResponse>(p, c)
			);
	}
}
