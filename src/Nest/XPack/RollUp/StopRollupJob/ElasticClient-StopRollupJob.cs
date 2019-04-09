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
		IStopRollupJobResponse StopRollupJob(Id id, Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		IStopRollupJobResponse StopRollupJob(IStopRollupJobRequest request);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		Task<IStopRollupJobResponse> StopRollupJobAsync(Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		Task<IStopRollupJobResponse> StopRollupJobAsync(IStopRollupJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IStopRollupJobResponse StopRollupJob(Id id, Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null) =>
			StopRollupJob(selector.InvokeOrDefault(new StopRollupJobDescriptor(id)));

		/// <inheritdoc />
		public IStopRollupJobResponse StopRollupJob(IStopRollupJobRequest request) =>
			Dispatch2<IStopRollupJobRequest, StopRollupJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IStopRollupJobResponse> StopRollupJobAsync(
			Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null,
			CancellationToken ct = default
		) => StopRollupJobAsync(selector.InvokeOrDefault(new StopRollupJobDescriptor(id)), ct);

		/// <inheritdoc />
		public Task<IStopRollupJobResponse> StopRollupJobAsync(IStopRollupJobRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IStopRollupJobRequest, IStopRollupJobResponse, StopRollupJobResponse>(request, request.RequestParameters, ct);
	}
}
