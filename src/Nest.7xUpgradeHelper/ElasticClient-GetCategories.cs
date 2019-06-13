using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves machine learning job results for one or more categories.
		/// </summary>
		public static GetCategoriesResponse GetCategories(this IElasticClient client,Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null);

		/// <inheritdoc />
		public static GetCategoriesResponse GetCategories(this IElasticClient client,IGetCategoriesRequest request);

		/// <inheritdoc />
		public static Task<GetCategoriesResponse> GetCategoriesAsync(this IElasticClient client,Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetCategoriesResponse> GetCategoriesAsync(this IElasticClient client,IGetCategoriesRequest request,
			CancellationToken ct = default
		);
	}

}
