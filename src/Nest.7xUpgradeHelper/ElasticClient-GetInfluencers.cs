using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves results for machine learning job influencers.
		/// </summary>
		public static GetInfluencersResponse GetInfluencers(this IElasticClient client,Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null);

		/// <inheritdoc />
		public static GetInfluencersResponse GetInfluencers(this IElasticClient client,IGetInfluencersRequest request);

		/// <inheritdoc />
		public static Task<GetInfluencersResponse> GetInfluencersAsync(this IElasticClient client,Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetInfluencersResponse> GetInfluencersAsync(this IElasticClient client,IGetInfluencersRequest request,
			CancellationToken ct = default
		);
	}

}
