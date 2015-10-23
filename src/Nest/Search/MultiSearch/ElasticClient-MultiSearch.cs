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
		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html
		/// </summary>
		/// <param name="multiSearchSelector">A descriptor that describes the search operations on the multi search api</param>
		IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, IMultiSearchRequest> multiSearchSelector);

		/// <inheritdoc/>
		IMultiSearchResponse MultiSearch(IMultiSearchRequest multiSearchRequest);

		/// <inheritdoc/>
		Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> multiSearchSelector);

		/// <inheritdoc/>
		Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest multiSearchRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, IMultiSearchRequest> multiSearchSelector) =>
			this.MultiSearch(multiSearchSelector?.Invoke(new Nest.MultiSearchDescriptor()));

		/// <inheritdoc/>
		public IMultiSearchResponse MultiSearch(IMultiSearchRequest multiSearchRequest) => 
			this.Dispatcher.Dispatch<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse>(
				multiSearchRequest,
				(p, d) => this.LowLevelDispatch.MsearchDispatch<MultiSearchResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, IMultiSearchRequest> multiSearchSelector) =>
			this.MultiSearchAsync(multiSearchSelector?.Invoke(new Nest.MultiSearchDescriptor()));

		/// <inheritdoc/>
		public Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest multiSearchRequest) =>
			this.Dispatcher.DispatchAsync<IMultiSearchRequest, MultiSearchRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				multiSearchRequest,
				(p, d) => this.LowLevelDispatch.MsearchDispatchAsync<MultiSearchResponse>(p, d)
			);
	}
}