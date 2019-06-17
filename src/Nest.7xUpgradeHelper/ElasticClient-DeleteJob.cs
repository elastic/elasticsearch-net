using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes a machine learning job.
		/// Before you can delete a job, you must delete the datafeeds that are associated with it, see DeleteDatafeed.
		/// Unless the force parameter is used, the job must be closed before it can be deleted.
		/// </summary>
		/// <remarks>
		/// It is not currently possible to delete multiple jobs, either using wildcards or a comma separated list.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteJobResponse DeleteJob(this IElasticClient client, Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null)
			=> client.MachineLearning.DeleteJob(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteJobResponse DeleteJob(this IElasticClient client, IDeleteJobRequest request)
			=> client.MachineLearning.DeleteJob(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteJobResponse> DeleteJobAsync(this IElasticClient client, Id jobId,
			Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteJobAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteJobResponse> DeleteJobAsync(this IElasticClient client, IDeleteJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.DeleteJobAsync(request, ct);
	}
}
