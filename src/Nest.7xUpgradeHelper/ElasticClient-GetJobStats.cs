using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves results for machine learning job influencers.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetJobStatsResponse GetJobStats(this IElasticClient client, Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null)
			=> client.MachineLearning.GetJobStats(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetJobStatsResponse GetJobStats(this IElasticClient client, IGetJobStatsRequest request)
			=> client.MachineLearning.GetJobStats(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetJobStatsResponse> GetJobStatsAsync(this IElasticClient client,
			Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetJobStatsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetJobStatsResponse> GetJobStatsAsync(this IElasticClient client, IGetJobStatsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetJobStatsAsync(request, ct);
	}
}
