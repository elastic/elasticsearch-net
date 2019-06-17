using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Forces any buffered data to be processed by the machine learning job.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static FlushJobResponse FlushJob(this IElasticClient client, Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null)
			=> client.MachineLearning.FlushJob(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static FlushJobResponse FlushJob(this IElasticClient client, IFlushJobRequest request)
			=> client.MachineLearning.FlushJob(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<FlushJobResponse> FlushJobAsync(this IElasticClient client, Id jobId,
			Func<FlushJobDescriptor, IFlushJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.FlushJobAsync(jobId,selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<FlushJobResponse> FlushJobAsync(this IElasticClient client, IFlushJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.FlushJobAsync(request, ct);
	}
}
