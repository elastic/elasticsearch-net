using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IStartRollupJobResponse StartRollupJob(Id id, Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null);

		/// <inheritdoc/>
		IStartRollupJobResponse StartRollupJob(IStartRollupJobRequest request);

		/// <inheritdoc/>
		Task<IStartRollupJobResponse> StartRollupJobAsync(Id id,
			Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null, CancellationToken cancellationToken = default);

		/// <inheritdoc/>
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
