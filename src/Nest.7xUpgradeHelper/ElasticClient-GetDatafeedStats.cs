using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves configuration information for machine learning datafeeds.
		/// </summary>
		public static GetDatafeedStatsResponse GetDatafeedStats(this IElasticClient client,Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null);

		/// <inheritdoc />
		public static GetDatafeedStatsResponse GetDatafeedStats(this IElasticClient client,IGetDatafeedStatsRequest request);

		/// <inheritdoc />
		public static Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(this IElasticClient client,Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(this IElasticClient client,IGetDatafeedStatsRequest request,
			CancellationToken ct = default
		);
	}

}
