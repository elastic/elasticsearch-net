using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetRollupJobResponse GetRollupJob(Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null);

		/// <inheritdoc/>
		IGetRollupJobResponse GetRollupJob(IGetRollupJobRequest request);

		/// <inheritdoc/>
		Task<IGetRollupJobResponse> GetRollupJobAsync(
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken cancellationToken = default);

		/// <inheritdoc/>
		Task<IGetRollupJobResponse> GetRollupJobAsync(IGetRollupJobRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetRollupJobResponse GetRollupJob(Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null) =>
			this.GetRollupJob(selector.InvokeOrDefault(new GetRollupJobDescriptor()));

		/// <inheritdoc/>
		public IGetRollupJobResponse GetRollupJob(IGetRollupJobRequest request) =>
			this.Dispatcher.Dispatch<IGetRollupJobRequest, GetRollupJobRequestParameters, GetRollupJobResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackRollupGetJobsDispatch<GetRollupJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetRollupJobResponse> GetRollupJobAsync(
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken cancellationToken = default
		)  =>
			this.GetRollupJobAsync(selector.InvokeOrDefault(new GetRollupJobDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetRollupJobResponse> GetRollupJobAsync(IGetRollupJobRequest request, CancellationToken cancellationToken = default) =>
			this.Dispatcher.DispatchAsync<IGetRollupJobRequest, GetRollupJobRequestParameters, GetRollupJobResponse, IGetRollupJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupGetJobsDispatchAsync<GetRollupJobResponse>(p, c)
			);
	}
}
