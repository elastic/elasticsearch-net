using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Start a machine learning datafeed.
		/// A datafeed must be started in order to retrieve data from Elasticsearch. A datafeed can be started and stopped multiple times throughout
		/// its lifecycle.
		/// </summary>
		public static StartDatafeedResponse StartDatafeed(this IElasticClient client,Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null);

		/// <inheritdoc />
		public static StartDatafeedResponse StartDatafeed(this IElasticClient client,IStartDatafeedRequest request);

		/// <inheritdoc />
		public static Task<StartDatafeedResponse> StartDatafeedAsync(this IElasticClient client,Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<StartDatafeedResponse> StartDatafeedAsync(this IElasticClient client,IStartDatafeedRequest request,
			CancellationToken ct = default
		);
	}

}
