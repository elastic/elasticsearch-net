using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a machine learning datafeed.
		/// You must create a job before you create a datafeed. You can associate only one datafeed to each job.
		/// </summary>
		public static PutDatafeedResponse PutDatafeed<T>(this IElasticClient client,Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null) where T : class;

		/// <inheritdoc />
		public static PutDatafeedResponse PutDatafeed(this IElasticClient client,IPutDatafeedRequest request);

		/// <inheritdoc />
		public static Task<PutDatafeedResponse> PutDatafeedAsync<T>(this IElasticClient client,Id datafeedId, Func<PutDatafeedDescriptor<T>, IPutDatafeedRequest> selector = null,
			CancellationToken ct = default
		) where T : class;

		/// <inheritdoc />
		public static Task<PutDatafeedResponse> PutDatafeedAsync(this IElasticClient client,IPutDatafeedRequest request, CancellationToken ct = default);
	}

}
