using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Rollup.StopJob(), please update this usage.")]
		public static StopRollupJobResponse StopRollupJob(this IElasticClient client, Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null
		)
			=> client.Rollup.StopJob(id, selector);

		[Obsolete("Moved to client.Rollup.StopJob(), please update this usage.")]
		public static StopRollupJobResponse StopRollupJob(this IElasticClient client, IStopRollupJobRequest request)
			=> client.Rollup.StopJob(request);

		[Obsolete("Moved to client.Rollup.StopJobAsync(), please update this usage.")]
		public static Task<StopRollupJobResponse> StopRollupJobAsync(this IElasticClient client, Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null, CancellationToken ct = default
		)
			=> client.Rollup.StopJobAsync(id, selector, ct);

		[Obsolete("Moved to client.Rollup.StopJobAsync(), please update this usage.")]
		public static Task<StopRollupJobResponse> StopRollupJobAsync(this IElasticClient client, IStopRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.StopJobAsync(request, ct);
	}
}
