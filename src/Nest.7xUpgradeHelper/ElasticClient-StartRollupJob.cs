using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Rollup.StartJob(), please update this usage.")]
		public static StartRollupJobResponse StartRollupJob(this IElasticClient client, Id id,
			Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null
		)
			=> client.Rollup.StartJob(id, selector);

		[Obsolete("Moved to client.Rollup.StartJob(), please update this usage.")]
		public static StartRollupJobResponse StartRollupJob(this IElasticClient client, IStartRollupJobRequest request)
			=> client.Rollup.StartJob(request);

		[Obsolete("Moved to client.Rollup.StartJobAsync(), please update this usage.")]
		public static Task<StartRollupJobResponse> StartRollupJobAsync(this IElasticClient client, Id id,
			Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null, CancellationToken ct = default
		)
			=> client.Rollup.StartJobAsync(id, selector, ct);

		[Obsolete("Moved to client.Rollup.StartJobAsync(), please update this usage.")]
		public static Task<StartRollupJobResponse> StartRollupJobAsync(this IElasticClient client, IStartRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.StartJobAsync(request, ct);
	}
}
