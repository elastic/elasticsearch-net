using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Stops an existing, started rollup job. If the job does not exist an exception will be thrown.
		/// Stopping an already stopped job has no action.
		/// </summary>
		StopRollupJobResponse StopRollupJob(Id id, Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		StopRollupJobResponse StopRollupJob(IStopRollupJobRequest request);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		Task<StopRollupJobResponse> StopRollupJobAsync(Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		Task<StopRollupJobResponse> StopRollupJobAsync(IStopRollupJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public StopRollupJobResponse StopRollupJob(Id id, Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null) =>
			StopRollupJob(selector.InvokeOrDefault(new StopRollupJobDescriptor(id)));

		/// <inheritdoc />
		public StopRollupJobResponse StopRollupJob(IStopRollupJobRequest request) =>
			DoRequest<IStopRollupJobRequest, StopRollupJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<StopRollupJobResponse> StopRollupJobAsync(
			Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null,
			CancellationToken ct = default
		) => StopRollupJobAsync(selector.InvokeOrDefault(new StopRollupJobDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<StopRollupJobResponse> StopRollupJobAsync(IStopRollupJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStopRollupJobRequest, StopRollupJobResponse, StopRollupJobResponse>(request, request.RequestParameters, ct);
	}
}
