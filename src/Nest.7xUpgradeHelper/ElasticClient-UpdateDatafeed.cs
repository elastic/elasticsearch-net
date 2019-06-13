using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Update a machine learning datafeed.
		/// </summary>
		public static UpdateDatafeedResponse UpdateDatafeed<T>(this IElasticClient client,Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null)
			where T : class;

		/// <inheritdoc />
		public static UpdateDatafeedResponse UpdateDatafeed(this IElasticClient client,IUpdateDatafeedRequest request);

		/// <inheritdoc />
		public static Task<UpdateDatafeedResponse> UpdateDatafeedAsync<T>(this IElasticClient client,Id datafeedId, Func<UpdateDatafeedDescriptor<T>, IUpdateDatafeedRequest> selector = null,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		public static Task<UpdateDatafeedResponse> UpdateDatafeedAsync(this IElasticClient client,IUpdateDatafeedRequest request,
			CancellationToken ct = default
		);
	}

}
