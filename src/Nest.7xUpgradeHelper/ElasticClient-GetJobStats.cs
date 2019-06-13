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
		public static GetJobStatsResponse GetJobStats(this IElasticClient client,Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null);

		/// <inheritdoc />
		public static GetJobStatsResponse GetJobStats(this IElasticClient client,IGetJobStatsRequest request);

		/// <inheritdoc />
		public static Task<GetJobStatsResponse> GetJobStatsAsync(this IElasticClient client,Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetJobStatsResponse> GetJobStatsAsync(this IElasticClient client,IGetJobStatsRequest request, CancellationToken ct = default);
	}

}
