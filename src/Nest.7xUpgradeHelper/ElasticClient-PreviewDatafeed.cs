using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Preview a machine learning datafeed.
		/// This preview shows the structure of the data that will be passed to the anomaly detection engine.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PreviewDatafeedResponse<T> PreviewDatafeed<T>(this IElasticClient client, Id datafeedId,
			Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null
		)
			=> client.MachineLearning.PreviewDatafeed<T>(datafeedId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PreviewDatafeedResponse<T> PreviewDatafeed<T>(this IElasticClient client, IPreviewDatafeedRequest request)
			=> client.MachineLearning.PreviewDatafeed<T>(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(this IElasticClient client, Id datafeedId,
			Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken ct = default
		)
			=> client.MachineLearning.PreviewDatafeedAsync<T>(datafeedId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(this IElasticClient client, IPreviewDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PreviewDatafeedAsync<T>(request, ct);
	}
}
