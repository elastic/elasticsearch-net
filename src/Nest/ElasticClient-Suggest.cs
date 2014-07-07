using System;
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
			return this.Dispatch<SuggestDescriptor<T>, SuggestRequestParameters, SuggestResponse>(
				selector,
				(p, d) => this.RawDispatch.SuggestDispatch<SuggestResponse>(p, d._Suggest)
			);
		}

		/// <inheritdoc />
		public Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, SuggestDescriptor<T>> selector)
			where T : class
		{
			return this.DispatchAsync<SuggestDescriptor<T>, SuggestRequestParameters, SuggestResponse, ISuggestResponse>(
				selector,
				(p, d) => this.RawDispatch.SuggestDispatchAsync<SuggestResponse>(p, d._Suggest)
			);
		}
	}
}