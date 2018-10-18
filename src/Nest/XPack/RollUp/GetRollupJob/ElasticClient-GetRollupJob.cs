using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets the configuration, stats and status of rollup jobs.
		/// It can return the details for a single job, or for all jobs.
		/// <para />
 		/// This API only returns active (both STARTED and STOPPED) jobs. If a job was created,
 		/// ran for a while then deleted, this API will not return any details about that job.
		/// </summary>
		IGetRollupJobResponse GetRollupJob(Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})"/>
		IGetRollupJobResponse GetRollupJob(IGetRollupJobRequest request);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})"/>
		Task<IGetRollupJobResponse> GetRollupJobAsync(
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})"/>
		Task<IGetRollupJobResponse> GetRollupJobAsync(IGetRollupJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken))  =>
			this.GetRollupJobAsync(selector.InvokeOrDefault(new GetRollupJobDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetRollupJobResponse> GetRollupJobAsync(IGetRollupJobRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetRollupJobRequest, GetRollupJobRequestParameters, GetRollupJobResponse, IGetRollupJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupGetJobsDispatchAsync<GetRollupJobResponse>(p, c)
			);
	}
}
