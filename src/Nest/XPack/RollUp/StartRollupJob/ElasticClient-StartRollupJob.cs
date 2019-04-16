using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Starts an existing, stopped rollup job. If the job does not exist an exception will be thrown.
		/// Starting an already started job has no action.
		/// </summary>
		IStartRollupJobResponse StartRollupJob(Id id, Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		IStartRollupJobResponse StartRollupJob(IStartRollupJobRequest request);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		Task<IStartRollupJobResponse> StartRollupJobAsync(Id id,
			Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null, CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		Task<IStartRollupJobResponse> StartRollupJobAsync(IStartRollupJobRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IStartRollupJobResponse StartRollupJob(Id id, Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null) =>
			StartRollupJob(selector.InvokeOrDefault(new StartRollupJobDescriptor(id)));

		/// <inheritdoc />
		public IStartRollupJobResponse StartRollupJob(IStartRollupJobRequest request) =>
			Dispatcher.Dispatch<IStartRollupJobRequest, StartRollupJobRequestParameters, StartRollupJobResponse>(
				request,
				(p, d) => LowLevelDispatch.RollupStartJobDispatch<StartRollupJobResponse>(p)
			);

		/// <inheritdoc />
		public Task<IStartRollupJobResponse> StartRollupJobAsync(
			Id id, Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null, CancellationToken cancellationToken = default
		) =>
			StartRollupJobAsync(selector.InvokeOrDefault(new StartRollupJobDescriptor(id)), cancellationToken);

		/// <inheritdoc />
		public Task<IStartRollupJobResponse> StartRollupJobAsync(IStartRollupJobRequest request, CancellationToken cancellationToken = default) =>
			Dispatcher.DispatchAsync<IStartRollupJobRequest, StartRollupJobRequestParameters, StartRollupJobResponse, IStartRollupJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.RollupStartJobDispatchAsync<StartRollupJobResponse>(p, c)
			);
	}
}
