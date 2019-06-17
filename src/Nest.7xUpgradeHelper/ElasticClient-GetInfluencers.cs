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
public static GetInfluencersResponse GetInfluencers(this IElasticClient client, Id jobId,
			Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null
		)
			=> client.MachineLearning.GetInfluencers(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetInfluencersResponse GetInfluencers(this IElasticClient client, IGetInfluencersRequest request)
			=> client.MachineLearning.GetInfluencers(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetInfluencersResponse> GetInfluencersAsync(this IElasticClient client, Id jobId,
			Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetInfluencersAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetInfluencersResponse> GetInfluencersAsync(this IElasticClient client, IGetInfluencersRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetInfluencersAsync(request, ct);
	}
}
