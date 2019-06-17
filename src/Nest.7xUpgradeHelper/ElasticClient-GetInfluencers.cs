using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetInfluencers(), please update this usage.")]
		public static GetInfluencersResponse GetInfluencers(this IElasticClient client, Id jobId,
			Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null
		)
			=> client.MachineLearning.GetInfluencers(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.GetInfluencers(), please update this usage.")]
		public static GetInfluencersResponse GetInfluencers(this IElasticClient client, IGetInfluencersRequest request)
			=> client.MachineLearning.GetInfluencers(request);

		[Obsolete("Moved to client.MachineLearning.GetInfluencersAsync(), please update this usage.")]
		public static Task<GetInfluencersResponse> GetInfluencersAsync(this IElasticClient client, Id jobId,
			Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetInfluencersAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetInfluencersAsync(), please update this usage.")]
		public static Task<GetInfluencersResponse> GetInfluencersAsync(this IElasticClient client, IGetInfluencersRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetInfluencersAsync(request, ct);
	}
}
