using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.OpenJob(), please update this usage.")]
		public static OpenJobResponse OpenJob(this IElasticClient client, Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null)
			=> client.MachineLearning.OpenJob(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.OpenJob(), please update this usage.")]
		public static OpenJobResponse OpenJob(this IElasticClient client, IOpenJobRequest request)
			=> client.MachineLearning.OpenJob(request);

		[Obsolete("Moved to client.MachineLearning.OpenJobAsync(), please update this usage.")]
		public static Task<OpenJobResponse> OpenJobAsync(this IElasticClient client, Id jobId,
			Func<OpenJobDescriptor, IOpenJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.OpenJobAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.OpenJobAsync(), please update this usage.")]
		public static Task<OpenJobResponse> OpenJobAsync(this IElasticClient client, IOpenJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.OpenJobAsync(request, ct);
	}
}
