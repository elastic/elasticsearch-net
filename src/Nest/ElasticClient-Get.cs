using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetResponse<T> Get<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class
		{
			return this.Dispatch<GetDescriptor<T>, GetRequestParameters, GetResponse<T>>(
				getSelector,
				(p, d) => this.RawDispatch.GetDispatch<GetResponse<T>>(p)
			);
		}

		/// <inheritdoc />
		public IGetResponse<T> Get<T>(IGetRequest getRequest) where T : class
		{
			return this.Dispatch<IGetRequest, GetRequestParameters, GetResponse<T>>(
				getRequest,
				(p, d) => this.RawDispatch.GetDispatch<GetResponse<T>>(p)
			);
		}

		/// <inheritdoc />
		public Task<IGetResponse<T>> GetAsync<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class
		{
			return this.DispatchAsync<GetDescriptor<T>, GetRequestParameters, GetResponse<T>, IGetResponse<T>>(
				getSelector,
				(p, d) => this.RawDispatch.GetDispatchAsync<GetResponse<T>>(p)
			);
		}

		/// <inheritdoc />
		public Task<IGetResponse<T>> GetAsync<T>(IGetRequest getRequest) where T : class
		{
			return this.DispatchAsync<IGetRequest, GetRequestParameters, GetResponse<T>, IGetResponse<T>>(
				getRequest,
				(p, d) => this.RawDispatch.GetDispatchAsync<GetResponse<T>>(p)
			);
		}

	}
}