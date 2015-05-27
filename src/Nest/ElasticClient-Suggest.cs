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
		public ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, SuggestDescriptor<T>> selector)
			where T : class
		{
			return this.Dispatcher.Dispatch<SuggestDescriptor<T>, SuggestRequestParameters, SuggestResponse>(
				selector,
				(p, d) => this.RawDispatch.SuggestDispatch<SuggestResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public ISuggestResponse Suggest(ISuggestRequest suggestRequest)
		{
			return this.Dispatcher.Dispatch<ISuggestRequest, SuggestRequestParameters, SuggestResponse>(
				suggestRequest,
				(p, d) => this.RawDispatch.SuggestDispatch<SuggestResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, SuggestDescriptor<T>> selector)
			where T : class
		{
			return this.Dispatcher.DispatchAsync<SuggestDescriptor<T>, SuggestRequestParameters, SuggestResponse, ISuggestResponse>(
				selector,
				(p, d) => this.RawDispatch.SuggestDispatchAsync<SuggestResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<ISuggestResponse> SuggestAsync(ISuggestRequest suggestRequest)
		{
			return this.Dispatcher.DispatchAsync<ISuggestRequest, SuggestRequestParameters, SuggestResponse, ISuggestResponse>(
				suggestRequest,
				(p, d) => this.RawDispatch.SuggestDispatchAsync<SuggestResponse>(p, d)
			);
		}

	}
}