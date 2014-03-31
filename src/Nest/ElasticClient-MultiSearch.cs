using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector)
		{
			return this.Dispatch<MultiSearchDescriptor, MultiSearchRequestParameters, MultiSearchResponse>(
				multiSearchSelector,
				(p, d) =>
				{
					string json = Serializer.SerializeMultiSearch(d);
					JsonConverter converter = CreateMultiSearchConverter(d);
					return this.RawDispatch.MsearchDispatch<MultiSearchResponse>(p, json, converter);
				}
			);
		}

		/// <inheritdoc />
		public Task<IMultiSearchResponse> MultiSearchAsync(
			Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector)
		{
			return this.DispatchAsync<MultiSearchDescriptor, MultiSearchRequestParameters, MultiSearchResponse, IMultiSearchResponse>(
				multiSearchSelector,
				(p, d) =>
				{
					string json = Serializer.SerializeMultiSearch(d);
					JsonConverter converter = CreateMultiSearchConverter(d);
					return this.RawDispatch.MsearchDispatchAsync<MultiSearchResponse>(p, json, converter);
				}
			);
		}

		private JsonConverter CreateMultiSearchConverter(MultiSearchDescriptor descriptor)
		{
			var multiSearchConverter = new MultiSearchConverter(_connectionSettings, descriptor);
			return multiSearchConverter;
		}
	}
}