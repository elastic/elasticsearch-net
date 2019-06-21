using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetCategories(), please update this usage.")]
		public static GetCategoriesResponse GetCategories(this IElasticClient client, Id jobId,
			Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null
		)
			=> client.MachineLearning.GetCategories(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.GetCategories(), please update this usage.")]
		public static GetCategoriesResponse GetCategories(this IElasticClient client, IGetCategoriesRequest request)
			=> client.MachineLearning.GetCategories(request);

		[Obsolete("Moved to client.MachineLearning.GetCategoriesAsync(), please update this usage.")]
		public static Task<GetCategoriesResponse> GetCategoriesAsync(this IElasticClient client, Id jobId,
			Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCategoriesAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetCategoriesAsync(), please update this usage.")]
		public static Task<GetCategoriesResponse> GetCategoriesAsync(this IElasticClient client, IGetCategoriesRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCategoriesAsync(request, ct);
	}
}
