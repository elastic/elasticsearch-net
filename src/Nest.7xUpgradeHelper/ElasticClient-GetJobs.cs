using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetJobs(), please update this usage.")]
		public static GetJobsResponse GetJobs(this IElasticClient client, Func<GetJobsDescriptor, IGetJobsRequest> selector = null)
			=> client.MachineLearning.GetJobs(selector);

		[Obsolete("Moved to client.MachineLearning.GetJobs(), please update this usage.")]
		public static GetJobsResponse GetJobs(this IElasticClient client, IGetJobsRequest request)
			=> client.MachineLearning.GetJobs(request);

		[Obsolete("Moved to client.MachineLearning.GetJobsAsync(), please update this usage.")]
		public static Task<GetJobsResponse> GetJobsAsync(this IElasticClient client, Func<GetJobsDescriptor, IGetJobsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetJobsAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetJobsAsync(), please update this usage.")]
		public static Task<GetJobsResponse> GetJobsAsync(this IElasticClient client, IGetJobsRequest request, CancellationToken ct = default)
			=> client.MachineLearning.GetJobsAsync(request, ct);
	}
}
