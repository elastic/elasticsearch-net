using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets the configuration, stats and status of rollup jobs.
		/// It can return the details for a single job, or for all jobs.
		/// <para />
		/// This API only returns active (both STARTED and STOPPED) jobs. If a job was created,
		/// ran for a while then deleted, this API will not return any details about that job.
		/// </summary>
		public static GetRollupJobResponse GetRollupJob(this IElasticClient client,Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		public static GetRollupJobResponse GetRollupJob(this IElasticClient client,IGetRollupJobRequest request);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		public static Task<GetRollupJobResponse> GetRollupJobAsync(this IElasticClient client,
			Func<GetRollupJobDescriptor, IGetRollupJobRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc cref="GetRollupJob(System.Func{Nest.GetRollupJobDescriptor,Nest.IGetRollupJobRequest})" />
		public static Task<GetRollupJobResponse> GetRollupJobAsync(this IElasticClient client,IGetRollupJobRequest request, CancellationToken ct = default);
	}

}
