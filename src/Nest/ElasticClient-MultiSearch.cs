using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.Resolvers;
using System.Reflection;
using System.Collections.Concurrent;

namespace Nest
{
	public partial class ElasticClient
	{
		public IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector)
		{
			return this.Dispatch<MultiSearchDescriptor, MultiSearchQueryString, MultiSearchResponse>(
				multiSearchSelector,
				(p, d) =>
				{
					var json = this.Serializer.SerializeMultiSearch(d);
					return this.RawDispatch.MsearchDispatch(p, json);
				},
				this.Serializer.DeserializeMultiSearchResponse
			);
		}

		public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector)
		{
			return this.DispatchAsync<MultiSearchDescriptor, MultiSearchQueryString, MultiSearchResponse, IMultiSearchResponse>(
				multiSearchSelector,
				(p, d) =>
				{
					var json = this.Serializer.SerializeMultiSearch(d);
					return this.RawDispatch.MsearchDispatchAsync(p, json);
				},
				this.Serializer.DeserializeMultiSearchResponse
			);
		}
	}
}
