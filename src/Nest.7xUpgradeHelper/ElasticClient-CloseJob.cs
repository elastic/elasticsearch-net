using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Closes a machine learning job.
		/// A closed job cannot receive data or perform analysis operations, but you can still explore and navigate results.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CloseJobResponse CloseJob(this IElasticClient client, Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null)
			=> client.MachineLearning.CloseJob(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CloseJobResponse CloseJob(this IElasticClient client, ICloseJobRequest request)
			=> client.MachineLearning.CloseJob(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CloseJobResponse> CloseJobAsync(this IElasticClient client, Id jobId,
			Func<CloseJobDescriptor, ICloseJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.CloseJobAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CloseJobResponse> CloseJobAsync(this IElasticClient client, ICloseJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.CloseJobAsync(request, ct);
	}
}
