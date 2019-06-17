using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.DeleteJob(), please update this usage.")]
		public static DeleteJobResponse DeleteJob(this IElasticClient client, Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null)
			=> client.MachineLearning.DeleteJob(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.DeleteJob(), please update this usage.")]
		public static DeleteJobResponse DeleteJob(this IElasticClient client, IDeleteJobRequest request)
			=> client.MachineLearning.DeleteJob(request);

		[Obsolete("Moved to client.MachineLearning.DeleteJobAsync(), please update this usage.")]
		public static Task<DeleteJobResponse> DeleteJobAsync(this IElasticClient client, Id jobId,
			Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteJobAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.DeleteJobAsync(), please update this usage.")]
		public static Task<DeleteJobResponse> DeleteJobAsync(this IElasticClient client, IDeleteJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.DeleteJobAsync(request, ct);
	}
}
