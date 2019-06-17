using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.FlushJob(), please update this usage.")]
		public static FlushJobResponse FlushJob(this IElasticClient client, Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null)
			=> client.MachineLearning.FlushJob(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.FlushJob(), please update this usage.")]
		public static FlushJobResponse FlushJob(this IElasticClient client, IFlushJobRequest request)
			=> client.MachineLearning.FlushJob(request);

		[Obsolete("Moved to client.MachineLearning.FlushJobAsync(), please update this usage.")]
		public static Task<FlushJobResponse> FlushJobAsync(this IElasticClient client, Id jobId,
			Func<FlushJobDescriptor, IFlushJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.FlushJobAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.FlushJobAsync(), please update this usage.")]
		public static Task<FlushJobResponse> FlushJobAsync(this IElasticClient client, IFlushJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.FlushJobAsync(request, ct);
	}
}
