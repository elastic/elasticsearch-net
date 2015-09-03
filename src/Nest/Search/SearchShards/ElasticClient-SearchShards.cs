using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ISearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> searchSelector) where T : class;

		/// <inheritdoc/>
		ISearchShardsResponse SearchShards(ISearchShardsRequest request);

		/// <inheritdoc/>
		Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> searchSelector)
			where T : class;

		/// <inheritdoc/>
		Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request);

	}
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ISearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> searchSelector) where T : class =>
			this.SearchShards(searchSelector?.Invoke(new SearchShardsDescriptor<T>()));

		/// <inheritdoc/>
		public ISearchShardsResponse SearchShards(ISearchShardsRequest request) => 
			this.Dispatcher.Dispatch<ISearchShardsRequest, SearchShardsRequestParameters, SearchShardsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SearchShardsDispatch<SearchShardsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> searchSelector)
			where T : class => 
			this.SearchShardsAsync(searchSelector?.Invoke(new SearchShardsDescriptor<T>()));

		/// <inheritdoc/>
		public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request) => 
			this.Dispatcher.DispatchAsync<ISearchShardsRequest, SearchShardsRequestParameters, SearchShardsResponse, ISearchShardsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.SearchShardsDispatchAsync<SearchShardsResponse>(p)
			);
	}
}