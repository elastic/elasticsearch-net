using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Sends data to a machine learning job for analysis.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PostJobDataResponse PostJobData(this IElasticClient client, Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector)
			=> client.MachineLearning.PostJobData(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PostJobDataResponse PostJobData(this IElasticClient client, IPostJobDataRequest request)
			=> client.MachineLearning.PostJobData(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PostJobDataResponse> PostJobDataAsync(this IElasticClient client, Id jobId,
			Func<PostJobDataDescriptor, IPostJobDataRequest> selector,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PostJobDataAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PostJobDataResponse> PostJobDataAsync(this IElasticClient client, IPostJobDataRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PostJobDataAsync(request, ct);
	}
}
