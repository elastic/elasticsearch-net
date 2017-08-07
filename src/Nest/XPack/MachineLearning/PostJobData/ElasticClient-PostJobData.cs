using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Sends data to a Machine Learning job for analysis.
		/// </summary>
		IPostJobDataResponse PostJobData(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector);

		/// <inheritdoc/>
		IPostJobDataResponse PostJobData(IPostJobDataRequest request);

		/// <inheritdoc/>
		Task<IPostJobDataResponse> PostJobDataAsync(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IPostJobDataResponse> PostJobDataAsync(IPostJobDataRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPostJobDataResponse PostJobData(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector) =>
			this.PostJobData(selector.InvokeOrDefault(new PostJobDataDescriptor(jobId)));

		/// <inheritdoc/>
		public IPostJobDataResponse PostJobData(IPostJobDataRequest request) =>
			this.Dispatcher.Dispatch<IPostJobDataRequest, PostJobDataRequestParameters, PostJobDataResponse>(
				request,
				this.LowLevelDispatch.XpackMlPostDataDispatch<PostJobDataResponse>
			);

		/// <inheritdoc/>
		public Task<IPostJobDataResponse> PostJobDataAsync(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PostJobDataAsync(selector.InvokeOrDefault(new PostJobDataDescriptor(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPostJobDataResponse> PostJobDataAsync(IPostJobDataRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPostJobDataRequest, PostJobDataRequestParameters, PostJobDataResponse, IPostJobDataResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlPostDataDispatchAsync<PostJobDataResponse>
			);
	}
}
