using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static DeleteJobResponse DeleteJob(this IElasticClient client,Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null);

		/// <inheritdoc />
		public static DeleteJobResponse DeleteJob(this IElasticClient client,IDeleteJobRequest request);

		/// <inheritdoc />
		public static Task<DeleteJobResponse> DeleteJobAsync(this IElasticClient client,Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteJobResponse> DeleteJobAsync(this IElasticClient client,IDeleteJobRequest request, CancellationToken ct = default);
	}

}
