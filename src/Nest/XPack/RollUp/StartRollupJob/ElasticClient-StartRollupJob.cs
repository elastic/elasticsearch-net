using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>Starts an existing, stopped rollup job. If the job does not exist an exception will be thrown.
		/// Starting an already started job has no action.
		/// </summary>
		IStartRollupJobResponse StartRollupJob(Id id, Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})"/>
		IStartRollupJobResponse StartRollupJob(IStartRollupJobRequest request);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})"/>
		Task<IStartRollupJobResponse> StartRollupJobAsync(Id id,
			Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null, CancellationToken cancellationToken = default);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})"/>
		Task<IStartRollupJobResponse> StartRollupJobAsync(IStartRollupJobRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IStartRollupJobResponse StartRollupJob(Id id, Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null) =>
			this.StartRollupJob(selector.InvokeOrDefault(new StartRollupJobDescriptor(id)));

		/// <inheritdoc/>
		public IStartRollupJobResponse StartRollupJob(IStartRollupJobRequest request) =>
			this.Dispatcher.Dispatch<IStartRollupJobRequest, StartRollupJobRequestParameters, StartRollupJobResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackRollupStartJobDispatch<StartRollupJobResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IStartRollupJobResponse> StartRollupJobAsync(
			Id id, Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null, CancellationToken cancellationToken = default
		)  =>
			this.StartRollupJobAsync(selector.InvokeOrDefault(new StartRollupJobDescriptor(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<IStartRollupJobResponse> StartRollupJobAsync(IStartRollupJobRequest request, CancellationToken cancellationToken = default) =>
			this.Dispatcher.DispatchAsync<IStartRollupJobRequest, StartRollupJobRequestParameters, StartRollupJobResponse, IStartRollupJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupStartJobDispatchAsync<StartRollupJobResponse>(p, c)
			);
	}
}
