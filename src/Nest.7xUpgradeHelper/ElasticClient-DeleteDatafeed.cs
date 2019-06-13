using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes an existing datafeed for a machine learning job.
		/// </summary>
		public static DeleteDatafeedResponse DeleteDatafeed(this IElasticClient client,Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null);

		/// <inheritdoc />
		public static DeleteDatafeedResponse DeleteDatafeed(this IElasticClient client,IDeleteDatafeedRequest request);

		/// <inheritdoc />
		public static Task<DeleteDatafeedResponse> DeleteDatafeedAsync(this IElasticClient client,Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<DeleteDatafeedResponse> DeleteDatafeedAsync(this IElasticClient client,IDeleteDatafeedRequest request,
			CancellationToken ct = default
		);
	}

}
