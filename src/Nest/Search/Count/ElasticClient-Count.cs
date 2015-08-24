using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICountResponse Count<T>(Func<CountDescriptor<T>, CountDescriptor<T>> countSelector = null) 
			where T : class
		{
			countSelector = countSelector ?? (s => s);
			return this.Dispatcher.Dispatch<CountDescriptor<T>, CountRequestParameters, CountResponse>(
				countSelector,
				(p, d) => this.LowLevelDispatch.CountDispatch<CountResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public ICountResponse Count<T>(ICountRequest countRequest) 
			where T : class
		{
			return this.Dispatcher.Dispatch<ICountRequest, CountRequestParameters, CountResponse>(
				countRequest,
				(p, d) => this.LowLevelDispatch.CountDispatch<CountResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, CountDescriptor<T>> countSelector = null)
			where T : class
		{
			countSelector = countSelector ?? (s => s);
			return this.Dispatcher.DispatchAsync<CountDescriptor<T>, CountRequestParameters, CountResponse, ICountResponse>(
				countSelector,
				(p, d) => this.LowLevelDispatch.CountDispatchAsync<CountResponse>(p, d)
			);
		}

		/// <inheritdoc/>
		public Task<ICountResponse> CountAsync<T>(ICountRequest countRequest)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<ICountRequest, CountRequestParameters, CountResponse, ICountResponse>(
				countRequest,
				(p, d) => this.LowLevelDispatch.CountDispatchAsync<CountResponse>(p, d)
			);
		}

	}
}