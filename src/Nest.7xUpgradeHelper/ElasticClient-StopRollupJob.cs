using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Stops an existing, started rollup job. If the job does not exist an exception will be thrown.
		/// Stopping an already stopped job has no action.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StopRollupJobResponse StopRollupJob(this IElasticClient client, Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null
		)
			=> client.Rollup.StopJob(id, selector);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StopRollupJobResponse StopRollupJob(this IElasticClient client, IStopRollupJobRequest request)
			=> client.Rollup.StopJob(request);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StopRollupJobResponse> StopRollupJobAsync(this IElasticClient client, Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null, CancellationToken ct = default
		)
			=> client.Rollup.StopJobAsync(id, selector, ct);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StopRollupJobResponse> StopRollupJobAsync(this IElasticClient client, IStopRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.StopJobAsync(request, ct);
	}
}
