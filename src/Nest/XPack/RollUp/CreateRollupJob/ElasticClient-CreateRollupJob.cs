using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a rollup job. The job will be created in a STOPPED state, and must be started with StartRollupJob API
		/// </summary>
		CreateRollupJobResponse CreateRollupJob<T>(Id id, Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector)
			where T : class;

		/// <inheritdoc cref="CreateRollupJob{T}" />
		CreateRollupJobResponse CreateRollupJob(ICreateRollupJobRequest request);

		/// <inheritdoc cref="CreateRollupJob{T}" />
		Task<CreateRollupJobResponse> CreateRollupJobAsync<T>(Id id,
			Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector, CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc cref="CreateRollupJob{T}" />
		Task<CreateRollupJobResponse> CreateRollupJobAsync(ICreateRollupJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CreateRollupJobResponse CreateRollupJob<T>(Id id, Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector)
			where T : class => CreateRollupJob(selector.InvokeOrDefault(new CreateRollupJobDescriptor<T>(id)));

		/// <inheritdoc />
		public CreateRollupJobResponse CreateRollupJob(ICreateRollupJobRequest request) =>
			DoRequest<ICreateRollupJobRequest, CreateRollupJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<CreateRollupJobResponse> CreateRollupJobAsync<T>(
			Id id,
			Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			CreateRollupJobAsync(selector.InvokeOrDefault(new CreateRollupJobDescriptor<T>(id)), ct);

		/// <inheritdoc />
		public Task<CreateRollupJobResponse> CreateRollupJobAsync(ICreateRollupJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ICreateRollupJobRequest, CreateRollupJobResponse>(request, request.RequestParameters, ct);
	}
}
