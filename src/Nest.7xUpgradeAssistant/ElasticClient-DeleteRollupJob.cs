using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Rollup.DeleteJob(), please update this usage.")]
		public static DeleteRollupJobResponse DeleteRollupJob(this IElasticClient client, Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null
		)
			=> client.Rollup.DeleteJob(id, selector);

		[Obsolete("Moved to client.Rollup.DeleteJob(), please update this usage.")]
		public static DeleteRollupJobResponse DeleteRollupJob(this IElasticClient client, IDeleteRollupJobRequest request)
			=> client.Rollup.DeleteJob(request);

		[Obsolete("Moved to client.Rollup.DeleteJobAsync(), please update this usage.")]
		public static Task<DeleteRollupJobResponse> DeleteRollupJobAsync(this IElasticClient client, Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null, CancellationToken ct = default
		)
			=> client.Rollup.DeleteJobAsync(id, selector, ct);

		[Obsolete("Moved to client.Rollup.DeleteJobAsync(), please update this usage.")]
		public static Task<DeleteRollupJobResponse> DeleteRollupJobAsync(this IElasticClient client, IDeleteRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.DeleteJobAsync(request, ct);
	}
}
