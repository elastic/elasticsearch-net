using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves machine learning filters
		/// </summary>
		GetFiltersResponse GetFilters(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null);


		/// <inheritdoc cref="GetFilters(System.Func{Nest.GetFiltersDescriptor,Nest.IGetFiltersRequest})" />
		GetFiltersResponse GetFilters(IGetFiltersRequest request);

		/// <inheritdoc cref="GetFilters(System.Func{Nest.GetFiltersDescriptor,Nest.IGetFiltersRequest})" />
		Task<GetFiltersResponse> GetFiltersAsync(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="GetFilters(System.Func{Nest.GetFiltersDescriptor,Nest.IGetFiltersRequest})" />
		Task<GetFiltersResponse> GetFiltersAsync(IGetFiltersRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetFiltersResponse GetFilters(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null) =>
			GetFilters(selector.InvokeOrDefault(new GetFiltersDescriptor()));

		/// <inheritdoc />
		public GetFiltersResponse GetFilters(IGetFiltersRequest request) =>
			DoRequest<IGetFiltersRequest, GetFiltersResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetFiltersResponse> GetFiltersAsync(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetFiltersAsync(selector.InvokeOrDefault(new GetFiltersDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<GetFiltersResponse> GetFiltersAsync(IGetFiltersRequest request, CancellationToken cancellationToken = default) =>
			DoRequestAsync<IGetFiltersRequest, GetFiltersResponse>(request, request.RequestParameters, cancellationToken);
	}
}
