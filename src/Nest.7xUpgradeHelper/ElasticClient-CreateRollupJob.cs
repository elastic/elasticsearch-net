using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a rollup job. The job will be created in a STOPPED state, and must be started with StartRollupJob API
		/// </summary>
		public static CreateRollupJobResponse CreateRollupJob<T>(this IElasticClient client,Id id, Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector)
			where T : class;

		/// <inheritdoc cref="CreateRollupJob{T}" />
		public static CreateRollupJobResponse CreateRollupJob(this IElasticClient client,ICreateRollupJobRequest request);

		/// <inheritdoc cref="CreateRollupJob{T}" />
		public static Task<CreateRollupJobResponse> CreateRollupJobAsync<T>(this IElasticClient client,Id id,
			Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector, CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc cref="CreateRollupJob{T}" />
		public static Task<CreateRollupJobResponse> CreateRollupJobAsync(this IElasticClient client,ICreateRollupJobRequest request, CancellationToken ct = default);
	}

}
