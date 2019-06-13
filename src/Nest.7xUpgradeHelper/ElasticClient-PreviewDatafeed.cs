using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Preview a machine learning datafeed.
		/// This preview shows the structure of the data that will be passed to the anomaly detection engine.
		/// </summary>
		public static PreviewDatafeedResponse<T> PreviewDatafeed<T>(this IElasticClient client,Id datafeedId, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null);

		/// <inheritdoc />
		public static PreviewDatafeedResponse<T> PreviewDatafeed<T>(this IElasticClient client,IPreviewDatafeedRequest request);

		/// <inheritdoc />
		public static Task<PreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(this IElasticClient client,Id datafeedId,
			Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<PreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(this IElasticClient client,IPreviewDatafeedRequest request,
			CancellationToken ct = default
		);
	}

}
