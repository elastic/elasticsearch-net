using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.PostJobData(), please update this usage.")]
		public static PostJobDataResponse PostJobData(this IElasticClient client, Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector)
			=> client.MachineLearning.PostJobData(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.PostJobData(), please update this usage.")]
		public static PostJobDataResponse PostJobData(this IElasticClient client, IPostJobDataRequest request)
			=> client.MachineLearning.PostJobData(request);

		[Obsolete("Moved to client.MachineLearning.PostJobDataAsync(), please update this usage.")]
		public static Task<PostJobDataResponse> PostJobDataAsync(this IElasticClient client, Id jobId,
			Func<PostJobDataDescriptor, IPostJobDataRequest> selector,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PostJobDataAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.PostJobDataAsync(), please update this usage.")]
		public static Task<PostJobDataResponse> PostJobDataAsync(this IElasticClient client, IPostJobDataRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PostJobDataAsync(request, ct);
	}
}
