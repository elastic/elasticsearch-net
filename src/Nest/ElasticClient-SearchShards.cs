using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, SearchShardsDescriptor<T>> searchSelector) where T : class
		{
			return this.Dispatch<SearchShardsDescriptor<T>, SearchShardsRequestParameters, SearchShardsResponse>(
				searchSelector,
				(p, d) => this.RawDispatch.SearchShardsDispatch<SearchShardsResponse>(p)
			);
		}
		
		/// <inheritdoc />
		public ISearchShardsResponse SearchShards(ISearchShardsRequest request)
		{
			return this.Dispatch<ISearchShardsRequest, SearchShardsRequestParameters, SearchShardsResponse>(
				request,
				(p, d) => this.RawDispatch.SearchShardsDispatch<SearchShardsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, SearchShardsDescriptor<T>> searchSelector)
			where T : class
		{
			return this.DispatchAsync<SearchShardsDescriptor<T>, SearchShardsRequestParameters, SearchShardsResponse, ISearchShardsResponse>(
				searchSelector,
				(p, d) => this.RawDispatch.SearchShardsDispatchAsync<SearchShardsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request)
		{
			return this.DispatchAsync<ISearchShardsRequest, SearchShardsRequestParameters, SearchShardsResponse, ISearchShardsResponse>(
				request,
				(p, d) => this.RawDispatch.SearchShardsDispatchAsync<SearchShardsResponse>(p)
			);
		}
		
	}
}