using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Starts an existing, stopped rollup job. If the job does not exist an exception will be thrown.
		/// Starting an already started job has no action.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartRollupJobResponse StartRollupJob(this IElasticClient client, Id id,
			Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null
		)
			=> client.Rollup.StartJob(id, selector);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartRollupJobResponse StartRollupJob(this IElasticClient client, IStartRollupJobRequest request)
			=> client.Rollup.StartJob(request);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartRollupJobResponse> StartRollupJobAsync(this IElasticClient client, Id id,
			Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null, CancellationToken ct = default
		)
			=> client.Rollup.StartJobAsync(id, selector, ct);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartRollupJobResponse> StartRollupJobAsync(this IElasticClient client, IStartRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.StartJobAsync(request, ct);
	}
}
