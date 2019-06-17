using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.PreviewDatafeed(), please update this usage.")]
		public static PreviewDatafeedResponse<T> PreviewDatafeed<T>(this IElasticClient client, Id datafeedId,
			Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null
		)
			=> client.MachineLearning.PreviewDatafeed<T>(datafeedId, selector);

		[Obsolete("Moved to client.MachineLearning.PreviewDatafeed(), please update this usage.")]
		public static PreviewDatafeedResponse<T> PreviewDatafeed<T>(this IElasticClient client, IPreviewDatafeedRequest request)
			=> client.MachineLearning.PreviewDatafeed<T>(request);

		[Obsolete("Moved to client.MachineLearning.PreviewDatafeedAsync(), please update this usage.")]
		public static Task<PreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(this IElasticClient client, Id datafeedId,
			Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken ct = default
		)
			=> client.MachineLearning.PreviewDatafeedAsync<T>(datafeedId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.PreviewDatafeedAsync(), please update this usage.")]
		public static Task<PreviewDatafeedResponse<T>> PreviewDatafeedAsync<T>(this IElasticClient client, IPreviewDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PreviewDatafeedAsync<T>(request, ct);
	}
}
