using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a rollup job. The job will be created in a STOPPED state, and must be started with StartRollupJob API
		/// </summary>
		ICreateRollupJobResponse CreateRollupJob<T>(Id id, Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector)
			where T : class;

		/// <inheritdoc cref="CreateRollupJob{T}"/>
		ICreateRollupJobResponse CreateRollupJob(ICreateRollupJobRequest request);

		/// <inheritdoc cref="CreateRollupJob{T}"/>
		Task<ICreateRollupJobResponse> CreateRollupJobAsync<T>(Id id,
			Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector, CancellationToken cancellationToken = default)
			where T : class;

		/// <inheritdoc cref="CreateRollupJob{T}"/>
		Task<ICreateRollupJobResponse> CreateRollupJobAsync(ICreateRollupJobRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICreateRollupJobResponse CreateRollupJob<T>(Id id, Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector)
			where T : class => this.CreateRollupJob(selector.InvokeOrDefault(new CreateRollupJobDescriptor<T>(id)));

		/// <inheritdoc/>
		public ICreateRollupJobResponse CreateRollupJob(ICreateRollupJobRequest request) =>
			this.Dispatcher.Dispatch<ICreateRollupJobRequest, CreateRollupJobRequestParameters, CreateRollupJobResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackRollupPutJobDispatch<CreateRollupJobResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<ICreateRollupJobResponse> CreateRollupJobAsync<T>(
			Id id, Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector, CancellationToken cancellationToken = default
		) where T : class =>
			this.CreateRollupJobAsync(selector.InvokeOrDefault(new CreateRollupJobDescriptor<T>(id)), cancellationToken);

		/// <inheritdoc/>
		public Task<ICreateRollupJobResponse> CreateRollupJobAsync(ICreateRollupJobRequest request, CancellationToken cancellationToken = default) =>
			this.Dispatcher.DispatchAsync<ICreateRollupJobRequest, CreateRollupJobRequestParameters, CreateRollupJobResponse, ICreateRollupJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackRollupPutJobDispatchAsync<CreateRollupJobResponse>(p, d, c)
			);
	}
}
