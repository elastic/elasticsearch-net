using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Rollup.CreateJob(), please update this usage.")]
		public static CreateRollupJobResponse CreateRollupJob<T>(this IElasticClient client, Id id,
			Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector
		)
			where T : class => client.Rollup.CreateJob(id, selector);

		[Obsolete("Moved to client.Rollup.CreateJob(), please update this usage.")]
		public static CreateRollupJobResponse CreateRollupJob(this IElasticClient client, ICreateRollupJobRequest request)
			=> client.Rollup.CreateJob(request);

		[Obsolete("Moved to client.Rollup.CreateJobAsync(), please update this usage.")]
		public static Task<CreateRollupJobResponse> CreateRollupJobAsync<T>(this IElasticClient client, Id id,
			Func<CreateRollupJobDescriptor<T>, ICreateRollupJobRequest> selector, CancellationToken ct = default
		)
			where T : class => client.Rollup.CreateJobAsync(id, selector, ct);

		[Obsolete("Moved to client.Rollup.CreateJobAsync(), please update this usage.")]
		public static Task<CreateRollupJobResponse> CreateRollupJobAsync(this IElasticClient client, ICreateRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.CreateJobAsync(request, ct);
	}
}
