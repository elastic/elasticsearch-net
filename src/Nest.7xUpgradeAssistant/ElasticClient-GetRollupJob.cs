using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Rollup.GetJob(), please update this usage.")]
		public static GetRollupJobResponse GetRollupJob(this IElasticClient client, Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null
		)
			=> client.Rollup.GetJob(selector);

		[Obsolete("Moved to client.Rollup.GetJob(), please update this usage.")]
		public static GetRollupJobResponse GetRollupJob(this IElasticClient client, IGetRollupJobRequest request)
			=> client.Rollup.GetJob(request);

		[Obsolete("Moved to client.Rollup.GetJobAsync(), please update this usage.")]
		public static Task<GetRollupJobResponse> GetRollupJobAsync(this IElasticClient client,
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken ct = default
		)
			=> client.Rollup.GetJobAsync(selector, ct);

		[Obsolete("Moved to client.Rollup.GetJobAsync(), please update this usage.")]
		public static Task<GetRollupJobResponse> GetRollupJobAsync(this IElasticClient client, IGetRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.GetJobAsync(request, ct);
	}
}
