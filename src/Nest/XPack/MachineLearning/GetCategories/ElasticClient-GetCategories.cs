using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves machine learning job results for one or more categories.
		/// </summary>
		GetCategoriesResponse GetCategories(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null);

		/// <inheritdoc />
		GetCategoriesResponse GetCategories(IGetCategoriesRequest request);

		/// <inheritdoc />
		Task<GetCategoriesResponse> GetCategoriesAsync(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetCategoriesResponse> GetCategoriesAsync(IGetCategoriesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetCategoriesResponse GetCategories(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null) =>
			GetCategories(selector.InvokeOrDefault(new GetCategoriesDescriptor(jobId)));

		/// <inheritdoc />
		public GetCategoriesResponse GetCategories(IGetCategoriesRequest request) =>
			DoRequest<IGetCategoriesRequest, GetCategoriesResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetCategoriesResponse> GetCategoriesAsync(
			Id jobId,
			Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null,
			CancellationToken ct = default
		) => GetCategoriesAsync(selector.InvokeOrDefault(new GetCategoriesDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<GetCategoriesResponse> GetCategoriesAsync(IGetCategoriesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetCategoriesRequest, GetCategoriesResponse, GetCategoriesResponse>(request, request.RequestParameters, ct);
	}
}
