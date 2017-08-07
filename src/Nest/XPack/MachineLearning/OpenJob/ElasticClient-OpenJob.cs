using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Opens a Machine Learning job.
		/// A job must be opened in order for it to be ready to receive and analyze data.
		/// A job can be opened and closed multiple times throughout its lifecycle.
		/// </summary>
		IOpenJobResponse OpenJob(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null);

		/// <inheritdoc/>
		IOpenJobResponse OpenJob(IOpenJobRequest request);

		/// <inheritdoc/>
		Task<IOpenJobResponse> OpenJobAsync(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IOpenJobResponse> OpenJobAsync(IOpenJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IOpenJobResponse OpenJob(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null) =>
			this.OpenJob(selector.InvokeOrDefault(new OpenJobDescriptor(jobId)));

		/// <inheritdoc/>
		public IOpenJobResponse OpenJob(IOpenJobRequest request) =>
			this.Dispatcher.Dispatch<IOpenJobRequest, OpenJobRequestParameters, OpenJobResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackMlOpenJobDispatch<OpenJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IOpenJobResponse> OpenJobAsync(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.OpenJobAsync(selector.InvokeOrDefault(new OpenJobDescriptor(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IOpenJobResponse> OpenJobAsync(IOpenJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IOpenJobRequest, OpenJobRequestParameters, OpenJobResponse, IOpenJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMlOpenJobDispatchAsync<OpenJobResponse>(p, c)
			);
	}
}
