using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		IGetRollupJobResponse GetRollupJob(IGetRollupJobRequest request);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		Task<IGetRollupJobResponse> GetRollupJobAsync(
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		Task<IGetRollupJobResponse> GetRollupJobAsync(IGetRollupJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetRollupJobResponse GetRollupJob(Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null) =>
			GetRollupJob(selector.InvokeOrDefault(new GetRollupJobDescriptor()));

		/// <inheritdoc />
		public IGetRollupJobResponse GetRollupJob(IGetRollupJobRequest request) =>
			Dispatcher.Dispatch<IGetRollupJobRequest, GetRollupJobRequestParameters, GetRollupJobResponse>(
				request,
				(p, d) => LowLevelDispatch.RollupGetJobsDispatch<GetRollupJobResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetRollupJobResponse> GetRollupJobAsync(
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetRollupJobAsync(selector.InvokeOrDefault(new GetRollupJobDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetRollupJobResponse> GetRollupJobAsync(IGetRollupJobRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetRollupJobRequest, GetRollupJobRequestParameters, GetRollupJobResponse, IGetRollupJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.RollupGetJobsDispatchAsync<GetRollupJobResponse>(p, c)
			);
	}
}
