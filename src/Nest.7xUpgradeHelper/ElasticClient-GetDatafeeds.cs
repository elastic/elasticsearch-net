using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetDatafeeds(), please update this usage.")]
		public static GetDatafeedsResponse GetDatafeeds(this IElasticClient client, Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null
		)
			=> client.MachineLearning.GetDatafeeds(selector);

		[Obsolete("Moved to client.MachineLearning.GetDatafeeds(), please update this usage.")]
		public static GetDatafeedsResponse GetDatafeeds(this IElasticClient client, IGetDatafeedsRequest request)
			=> client.MachineLearning.GetDatafeeds(request);

		[Obsolete("Moved to client.MachineLearning.GetDatafeedsAsync(), please update this usage.")]
		public static Task<GetDatafeedsResponse> GetDatafeedsAsync(this IElasticClient client,
			Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetDatafeedsAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetDatafeedsAsync(), please update this usage.")]
		public static Task<GetDatafeedsResponse> GetDatafeedsAsync(this IElasticClient client, IGetDatafeedsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetDatafeedsAsync(request, ct);
	}
}
