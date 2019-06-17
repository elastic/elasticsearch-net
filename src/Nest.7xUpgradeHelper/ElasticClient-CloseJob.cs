using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.CloseJob(), please update this usage.")]
		public static CloseJobResponse CloseJob(this IElasticClient client, Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null)
			=> client.MachineLearning.CloseJob(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.CloseJob(), please update this usage.")]
		public static CloseJobResponse CloseJob(this IElasticClient client, ICloseJobRequest request)
			=> client.MachineLearning.CloseJob(request);

		[Obsolete("Moved to client.MachineLearning.CloseJobAsync(), please update this usage.")]
		public static Task<CloseJobResponse> CloseJobAsync(this IElasticClient client, Id jobId,
			Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.CloseJobAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.CloseJobAsync(), please update this usage.")]
		public static Task<CloseJobResponse> CloseJobAsync(this IElasticClient client, ICloseJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.CloseJobAsync(request, ct);
	}
}
