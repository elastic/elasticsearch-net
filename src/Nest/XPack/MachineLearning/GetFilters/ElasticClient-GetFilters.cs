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
		IGetFiltersResponse GetFilters(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null);


		/// <inheritdoc cref="GetFilters(System.Func{Nest.GetFiltersDescriptor,Nest.IGetFiltersRequest})" />
		IGetFiltersResponse GetFilters(IGetFiltersRequest request);

		/// <inheritdoc cref="GetFilters(System.Func{Nest.GetFiltersDescriptor,Nest.IGetFiltersRequest})" />
		Task<IGetFiltersResponse> GetFiltersAsync(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetFilters(System.Func{Nest.GetFiltersDescriptor,Nest.IGetFiltersRequest})" />
		Task<IGetFiltersResponse> GetFiltersAsync(IGetFiltersRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetFiltersResponse GetFilters(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null) =>
			GetFilters(selector.InvokeOrDefault(new GetFiltersDescriptor()));

		/// <inheritdoc />
		public IGetFiltersResponse GetFilters(IGetFiltersRequest request) =>
			Dispatcher.Dispatch<IGetFiltersRequest, GetFiltersRequestParameters, GetFiltersResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackMlGetFiltersDispatch<GetFiltersResponse>(p)
			);

		/// <inheritdoc />
		public Task<IGetFiltersResponse> GetFiltersAsync(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetFiltersAsync(selector.InvokeOrDefault(new GetFiltersDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetFiltersResponse> GetFiltersAsync(IGetFiltersRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetFiltersRequest, GetFiltersRequestParameters, GetFiltersResponse, IGetFiltersResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackMlGetFiltersDispatchAsync<GetFiltersResponse>(p, c)
			);
	}
}
