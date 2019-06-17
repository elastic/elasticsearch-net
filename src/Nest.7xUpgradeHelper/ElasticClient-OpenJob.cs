using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Opens a machine learning job.
		/// A job must be opened in order for it to be ready to receive and analyze data.
		/// A job can be opened and closed multiple times throughout its lifecycle.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static OpenJobResponse OpenJob(this IElasticClient client, Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null)
			=> client.MachineLearning.OpenJob(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static OpenJobResponse OpenJob(this IElasticClient client, IOpenJobRequest request)
			=> client.MachineLearning.OpenJob(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<OpenJobResponse> OpenJobAsync(this IElasticClient client, Id jobId,
			Func<OpenJobDescriptor, IOpenJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.OpenJobAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<OpenJobResponse> OpenJobAsync(this IElasticClient client, IOpenJobRequest request, CancellationToken ct = default)
			=> client.MachineLearning.OpenJobAsync(request, ct);
	}
}
