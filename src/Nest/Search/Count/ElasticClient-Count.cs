using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The count API allows to easily execute a query and get the number of matches for that query. 
		/// It can be executed across one or more indices and across one or more types. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-count.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query</typeparam>
		/// <param name="countSelector">An optional descriptor to further describe the count operation</param>
		ICountResponse Count<T>(Func<CountDescriptor<T>, ICountRequest> countSelector = null)
			where T : class;

		/// <inheritdoc/>
		ICountResponse Count<T>(ICountRequest countRequest)
			where T : class;

		/// <inheritdoc/>
		Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, ICountRequest> countSelector = null)
			where T : class;

		/// <inheritdoc/>
		Task<ICountResponse> CountAsync<T>(ICountRequest countRequest)
			where T : class;
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICountResponse Count<T>(Func<CountDescriptor<T>, ICountRequest> countSelector = null)
			where T : class =>
			this.Count<T>(countSelector.InvokeOrDefault(new CountDescriptor<T>()));

		/// <inheritdoc/>
		public ICountResponse Count<T>(ICountRequest countRequest)
			where T : class => 
			this.Dispatcher.Dispatch<ICountRequest, CountRequestParameters, CountResponse>(
				countRequest,
				this.LowLevelDispatch.CountDispatch<CountResponse>
			);

		/// <inheritdoc/>
		public Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, ICountRequest> countSelector = null)
			where T : class => 
			this.CountAsync<T>(countSelector.InvokeOrDefault(new CountDescriptor<T>()));

		/// <inheritdoc/>
		public Task<ICountResponse> CountAsync<T>(ICountRequest countRequest)
			where T : class => 
			this.Dispatcher.DispatchAsync<ICountRequest, CountRequestParameters, CountResponse, ICountResponse>(
				countRequest,
				this.LowLevelDispatch.CountDispatchAsync<CountResponse>
			);
	}
}