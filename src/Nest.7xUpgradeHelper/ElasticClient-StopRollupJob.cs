using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Stops an existing, started rollup job. If the job does not exist an exception will be thrown.
		/// Stopping an already stopped job has no action.
		/// </summary>
		public static StopRollupJobResponse StopRollupJob(this IElasticClient client,Id id, Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		public static StopRollupJobResponse StopRollupJob(this IElasticClient client,IStopRollupJobRequest request);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		public static Task<StopRollupJobResponse> StopRollupJobAsync(this IElasticClient client,Id id,
			Func<StopRollupJobDescriptor, IStopRollupJobRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc cref="StopRollupJob(Nest.Id,System.Func{Nest.StopRollupJobDescriptor,Nest.IStopRollupJobRequest})" />
		public static Task<StopRollupJobResponse> StopRollupJobAsync(this IElasticClient client,IStopRollupJobRequest request, CancellationToken ct = default);
	}

}
