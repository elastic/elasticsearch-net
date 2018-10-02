using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IStopRollupJobResponse StopRollupJob(Id id, Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null);

		/// <inheritdoc/>
		IStopRollupJobResponse StopRollupJob(IStopRollupJobRequest request);

		/// <inheritdoc/>
		Task<IStopRollupJobResponse> StopRollupJobAsync(Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null, CancellationToken cancellationToken = default);

		/// <inheritdoc/>
		Task<IStopRollupJobResponse> StopRollupJobAsync(IStopRollupJobRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IStopRollupJobResponse StopRollupJob(Id id, Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null) =>
			this.StopRollupJob(selector.InvokeOrDefault(new StopRollupJobDescriptor(id)));

		/// <inheritdoc/>
		public IStopRollupJobResponse StopRollupJob(IStopRollupJobRequest request) =>
			this.Dispatcher.Dispatch<IStopRollupJobRequest, StopRollupJobRequestParameters, StopRollupJobResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackRollupStopJobDispatch<StopRollupJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IStopRollupJobResponse> StopRollupJobAsync(
			Id id, Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null, CancellationToken cancellationToken = default
		)  =>
			this.StopRollupJobAsync(selector.InvokeOrDefault(new StopRollupJobDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IStopRollupJobResponse> StopRollupJobAsync(IStopRollupJobRequest request, CancellationToken cancellationToken = default) =>
			this.Dispatcher.DispatchAsync<IStopRollupJobRequest, StopRollupJobRequestParameters, StopRollupJobResponse, IStopRollupJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupStopJobDispatchAsync<StopRollupJobResponse>(p, c)
			);
	}
}
