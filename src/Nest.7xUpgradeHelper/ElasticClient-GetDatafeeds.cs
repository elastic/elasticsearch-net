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
		public static GetDatafeedsResponse GetDatafeeds(this IElasticClient client,Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null);

		/// <inheritdoc />
		public static GetDatafeedsResponse GetDatafeeds(this IElasticClient client,IGetDatafeedsRequest request);

		/// <inheritdoc />
		public static Task<GetDatafeedsResponse> GetDatafeedsAsync(this IElasticClient client,Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetDatafeedsResponse> GetDatafeedsAsync(this IElasticClient client,IGetDatafeedsRequest request, CancellationToken ct = default);
	}

}
