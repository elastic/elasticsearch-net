using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.UpdateJob(), please update this usage.")]
		public static UpdateJobResponse UpdateJob<T>(this IElasticClient client, Id jobId,
			Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null
		) where T : class
			=> client.MachineLearning.UpdateJob(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.UpdateJob(), please update this usage.")]
		public static UpdateJobResponse UpdateJob(this IElasticClient client, IUpdateJobRequest request)
			=> client.MachineLearning.UpdateJob(request);

		[Obsolete("Moved to client.MachineLearning.UpdateJobAsync(), please update this usage.")]
		public static Task<UpdateJobResponse> UpdateJobAsync<T>(this IElasticClient client, Id jobId,
			Func<UpdateJobDescriptor<T>, IUpdateJobRequest> selector = null,
			CancellationToken ct = default
		) where T : class
			=> client.MachineLearning.UpdateJobAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.UpdateJobAsync(), please update this usage.")]
		public static Task<UpdateJobResponse> UpdateJobAsync(this IElasticClient client, IUpdateJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.UpdateJobAsync(request, ct);
	}
}
