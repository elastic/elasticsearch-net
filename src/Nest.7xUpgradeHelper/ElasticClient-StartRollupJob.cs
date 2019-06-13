using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Starts an existing, stopped rollup job. If the job does not exist an exception will be thrown.
		/// Starting an already started job has no action.
		/// </summary>
		public static StartRollupJobResponse StartRollupJob(this IElasticClient client,Id id, Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		public static StartRollupJobResponse StartRollupJob(this IElasticClient client,IStartRollupJobRequest request);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		public static Task<StartRollupJobResponse> StartRollupJobAsync(this IElasticClient client,Id id,
			Func<StartRollupJobDescriptor, IStartRollupJobRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc cref="StartRollupJob(Nest.Id,System.Func{Nest.StartRollupJobDescriptor,Nest.IStartRollupJobRequest})" />
		public static Task<StartRollupJobResponse> StartRollupJobAsync(this IElasticClient client,IStartRollupJobRequest request, CancellationToken ct = default);
	}

}
