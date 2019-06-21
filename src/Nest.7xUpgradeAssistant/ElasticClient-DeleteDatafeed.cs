using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.DeleteDatafeed(), please update this usage.")]
		public static DeleteDatafeedResponse DeleteDatafeed(this IElasticClient client, Id datafeedId,
			Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null
		)
			=> client.MachineLearning.DeleteDatafeed(datafeedId, selector);

		[Obsolete("Moved to client.MachineLearning.DeleteDatafeed(), please update this usage.")]
		public static DeleteDatafeedResponse DeleteDatafeed(this IElasticClient client, IDeleteDatafeedRequest request)
			=> client.MachineLearning.DeleteDatafeed(request);

		[Obsolete("Moved to client.MachineLearning.DeleteDatafeedAsync(), please update this usage.")]
		public static Task<DeleteDatafeedResponse> DeleteDatafeedAsync(this IElasticClient client, Id datafeedId,
			Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteDatafeedAsync(datafeedId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.DeleteDatafeedAsync(), please update this usage.")]
		public static Task<DeleteDatafeedResponse> DeleteDatafeedAsync(this IElasticClient client, IDeleteDatafeedRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteDatafeedAsync(request, ct);
	}
}
