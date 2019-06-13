using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes an existing rollup job. The job can be started or stopped, in both cases it will be deleted.
		/// Attempting to delete a non-existing job will throw an exception
		/// </summary>
		public static DeleteRollupJobResponse DeleteRollupJob(this IElasticClient client,Id id, Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		public static DeleteRollupJobResponse DeleteRollupJob(this IElasticClient client,IDeleteRollupJobRequest request);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		public static Task<DeleteRollupJobResponse> DeleteRollupJobAsync(this IElasticClient client,Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		public static Task<DeleteRollupJobResponse> DeleteRollupJobAsync(this IElasticClient client,IDeleteRollupJobRequest request, CancellationToken ct = default);
	}

}
