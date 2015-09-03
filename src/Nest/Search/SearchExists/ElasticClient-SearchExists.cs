using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using SearchExistConverter = Func<IApiCallDetails, Stream, ExistsResponse>;

	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IExistsResponse SearchExists<T>(Func<SearchExistsDescriptor<T>, ISearchExistsRequest> selector) where T : class;

		/// <inheritdoc/>
		IExistsResponse SearchExists(ISearchExistsRequest indexRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> SearchExistsAsync<T>(Func<SearchExistsDescriptor<T>, ISearchExistsRequest> selector) where T : class;

		/// <inheritdoc/>
		Task<IExistsResponse> SearchExistsAsync(ISearchExistsRequest indexRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IExistsResponse SearchExists<T>(Func<SearchExistsDescriptor<T>, ISearchExistsRequest> selector)
			where T : class =>
			this.SearchExists(selector?.Invoke(new SearchExistsDescriptor<T>()));

		/// <inheritdoc/>
		public IExistsResponse SearchExists(ISearchExistsRequest indexRequest) => 
			this.Dispatcher.Dispatch<ISearchExistsRequest, SearchExistsRequestParameters, ExistsResponse>(
				indexRequest,
				new SearchExistConverter(DeserializeExistsResponse),
				this.LowLevelDispatch.SearchExistsDispatch<ExistsResponse>
			);

		/// <inheritdoc/>
		public Task<IExistsResponse> SearchExistsAsync<T>(Func<SearchExistsDescriptor<T>, ISearchExistsRequest> selector)
			where T : class => 
			this.SearchExistsAsync(selector?.Invoke(new SearchExistsDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IExistsResponse> SearchExistsAsync(ISearchExistsRequest indexRequest) => 
			this.Dispatcher.DispatchAsync<ISearchExistsRequest, SearchExistsRequestParameters, ExistsResponse, IExistsResponse>(
				indexRequest, 
				new SearchExistConverter(DeserializeExistsResponse),
				this.LowLevelDispatch.SearchExistsDispatchAsync<ExistsResponse>
			);
	}
}