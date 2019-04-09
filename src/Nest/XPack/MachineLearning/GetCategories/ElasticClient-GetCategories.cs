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
		IGetCategoriesResponse GetCategories(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null);

		/// <inheritdoc />
		IGetCategoriesResponse GetCategories(IGetCategoriesRequest request);

		/// <inheritdoc />
		Task<IGetCategoriesResponse> GetCategoriesAsync(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetCategoriesResponse> GetCategoriesAsync(IGetCategoriesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetCategoriesResponse GetCategories(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null) =>
			GetCategories(selector.InvokeOrDefault(new GetCategoriesDescriptor(jobId)));

		/// <inheritdoc />
		public IGetCategoriesResponse GetCategories(IGetCategoriesRequest request) =>
			Dispatch2<IGetCategoriesRequest, GetCategoriesResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetCategoriesResponse> GetCategoriesAsync(
			Id jobId,
			Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null,
			CancellationToken ct = default
		) => GetCategoriesAsync(selector.InvokeOrDefault(new GetCategoriesDescriptor(jobId)), ct);

		/// <inheritdoc />
		public Task<IGetCategoriesResponse> GetCategoriesAsync(IGetCategoriesRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetCategoriesRequest, IGetCategoriesResponse, GetCategoriesResponse>(request, request.RequestParameters, ct);
	}
}
