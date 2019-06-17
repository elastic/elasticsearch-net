using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a rollup job. The job will be created in a STOPPED state, and must be started with StartRollupJob API
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CreateRollupJobResponse CreateRollupJob<T>(this IElasticClient client, Id id,
			Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector
		)
			where T : class => client.Rollup.CreateJob(id, selector);

		/// <inheritdoc cref="CreateRollupJob{T}" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CreateRollupJobResponse CreateRollupJob(this IElasticClient client, ICreateRollupJobRequest request)
			=> client.Rollup.CreateJob(request);

		/// <inheritdoc cref="CreateRollupJob{T}" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CreateRollupJobResponse> CreateRollupJobAsync<T>(this IElasticClient client, Id id,
			Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector, CancellationToken ct = default
		)
			where T : class => client.Rollup.CreateJobAsync(id, selector, ct);

		/// <inheritdoc cref="CreateRollupJob{T}" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CreateRollupJobResponse> CreateRollupJobAsync(this IElasticClient client, ICreateRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.CreateJobAsync(request, ct);
	}
}
