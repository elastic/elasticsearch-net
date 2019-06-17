using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves machine learning job results for one or more categories.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetCategoriesResponse GetCategories(this IElasticClient client, Id jobId,
			Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null
		)
			=> client.MachineLearning.GetCategories(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetCategoriesResponse GetCategories(this IElasticClient client, IGetCategoriesRequest request)
			=> client.MachineLearning.GetCategories(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetCategoriesResponse> GetCategoriesAsync(this IElasticClient client, Id jobId,
			Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCategoriesAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetCategoriesResponse> GetCategoriesAsync(this IElasticClient client, IGetCategoriesRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCategoriesAsync(request, ct);
	}
}
