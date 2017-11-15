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

		/// <inheritdoc/>
		IGetCategoriesResponse GetCategories(IGetCategoriesRequest request);

		/// <inheritdoc/>
		Task<IGetCategoriesResponse> GetCategoriesAsync(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetCategoriesResponse> GetCategoriesAsync(IGetCategoriesRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetCategoriesResponse GetCategories(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null) =>
			this.GetCategories(selector.InvokeOrDefault(new GetCategoriesDescriptor(jobId)));

		/// <inheritdoc/>
		public IGetCategoriesResponse GetCategories(IGetCategoriesRequest request) =>
			this.Dispatcher.Dispatch<IGetCategoriesRequest, GetCategoriesRequestParameters, GetCategoriesResponse>(
				request,
				this.LowLevelDispatch.XpackMlGetCategoriesDispatch<GetCategoriesResponse>
			);

		/// <inheritdoc/>
		public Task<IGetCategoriesResponse> GetCategoriesAsync(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetCategoriesAsync(selector.InvokeOrDefault(new GetCategoriesDescriptor(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetCategoriesResponse> GetCategoriesAsync(IGetCategoriesRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetCategoriesRequest, GetCategoriesRequestParameters, GetCategoriesResponse, IGetCategoriesResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlGetCategoriesDispatchAsync<GetCategoriesResponse>
			);
	}
}
