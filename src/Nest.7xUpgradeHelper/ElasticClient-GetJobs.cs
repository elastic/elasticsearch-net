using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves machine learning job configuration information
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetJobsResponse GetJobs(this IElasticClient client, Func<GetJobsDescriptor, IGetJobsRequest> selector = null)
			=> client.MachineLearning.GetJobs(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetJobsResponse GetJobs(this IElasticClient client, IGetJobsRequest request)
			=> client.MachineLearning.GetJobs(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetJobsResponse> GetJobsAsync(this IElasticClient client, Func<GetJobsDescriptor, IGetJobsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetJobsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetJobsResponse> GetJobsAsync(this IElasticClient client, IGetJobsRequest request, CancellationToken ct = default)
			=> client.MachineLearning.GetJobsAsync(request, ct);
	}
}
