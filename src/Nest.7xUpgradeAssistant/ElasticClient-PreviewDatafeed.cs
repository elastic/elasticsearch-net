using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.PreviewDatafeed(), please update this usage.")]
		public static PreviewDatafeedResponse<TDocument> PreviewDatafeed<TDocument>(this IElasticClient client, Id datafeedId,
			Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null
		) where TDocument : class => client.MachineLearning.PreviewDatafeed<TDocument>(datafeedId, selector);

		[Obsolete("Moved to client.MachineLearning.PreviewDatafeed(), please update this usage.")]
		public static PreviewDatafeedResponse<TDocument> PreviewDatafeed<TDocument>(this IElasticClient client, IPreviewDatafeedRequest request)
			where TDocument : class => client.MachineLearning.PreviewDatafeed<TDocument>(request);

		[Obsolete("Moved to client.MachineLearning.PreviewDatafeedAsync(), please update this usage.")]
		public static Task<PreviewDatafeedResponse<TDocument>> PreviewDatafeedAsync<TDocument>(this IElasticClient client, Id datafeedId,
			Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken ct = default
		) where TDocument : class => client.MachineLearning.PreviewDatafeedAsync<TDocument>(datafeedId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.PreviewDatafeedAsync(), please update this usage.")]
		public static Task<PreviewDatafeedResponse<TDocument>> PreviewDatafeedAsync<TDocument>(this IElasticClient client,
			IPreviewDatafeedRequest request,
			CancellationToken ct = default
		) where TDocument : class => client.MachineLearning.PreviewDatafeedAsync<TDocument>(request, ct);
	}
}
