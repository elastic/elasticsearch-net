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
		GetRollupJobResponse GetRollupJob(Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		GetRollupJobResponse GetRollupJob(IGetRollupJobRequest request);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		Task<GetRollupJobResponse> GetRollupJobAsync(
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		Task<GetRollupJobResponse> GetRollupJobAsync(IGetRollupJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetRollupJobResponse GetRollupJob(Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null) =>
			GetRollupJob(selector.InvokeOrDefault(new GetRollupJobDescriptor()));

		/// <inheritdoc />
		public GetRollupJobResponse GetRollupJob(IGetRollupJobRequest request) =>
			DoRequest<IGetRollupJobRequest, GetRollupJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetRollupJobResponse> GetRollupJobAsync(
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null,
			CancellationToken ct = default
		) => GetRollupJobAsync(selector.InvokeOrDefault(new GetRollupJobDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetRollupJobResponse> GetRollupJobAsync(IGetRollupJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetRollupJobRequest, GetRollupJobResponse, GetRollupJobResponse>
				(request, request.RequestParameters, ct);
	}
}
