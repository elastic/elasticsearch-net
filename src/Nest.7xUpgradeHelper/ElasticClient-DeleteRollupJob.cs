using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes an existing rollup job. The job can be started or stopped, in both cases it will be deleted.
		/// Attempting to delete a non-existing job will throw an exception
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteRollupJobResponse DeleteRollupJob(this IElasticClient client, Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null
		)
			=> client.Rollup.DeleteJob(id, selector);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteRollupJobResponse DeleteRollupJob(this IElasticClient client, IDeleteRollupJobRequest request)
			=> client.Rollup.DeleteJob(request);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteRollupJobResponse> DeleteRollupJobAsync(this IElasticClient client, Id id,
			Func<DeleteRollupJobDescriptor, IDeleteRollupJobRequest> selector = null, CancellationToken ct = default
		)
			=> client.Rollup.DeleteJobAsync(id, selector, ct);

		/// <inheritdoc cref="DeleteRollupJob(Nest.Id,System.Func{Nest.DeleteRollupJobDescriptor,Nest.IDeleteRollupJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteRollupJobResponse> DeleteRollupJobAsync(this IElasticClient client, IDeleteRollupJobRequest request,
			CancellationToken ct = default
		)
			=> client.Rollup.DeleteJobAsync(request, ct);
	}
}
