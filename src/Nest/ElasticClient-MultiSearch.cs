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
			multiSearchSelector.ThrowIfNull("multiSearchSelector");
			var descriptor = multiSearchSelector(new MultiSearchDescriptor());

			var multiSearchConverter = new MultiSearchConverter(this._connectionSettings, descriptor);
			var pathInfo = ((IPathInfo<MultiSearchQueryString>) descriptor).ToPathInfo(this._connectionSettings);
			var json = this.Serializer.SerializeMultiSearch(descriptor);
			var status = this.RawDispatch.MsearchDispatch(pathInfo, json);

			var multiSearchResponse = this.Serializer.DeserializeInternal<MultiSearchResponse>(
				status, 
				piggyBackJsonConverter: multiSearchConverter
			);
			return multiSearchResponse;
		}

		public Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector)
		{
			multiSearchSelector.ThrowIfNull("multiSearchSelector");
			var descriptor = multiSearchSelector(new MultiSearchDescriptor());

			var multiSearchConverter = new MultiSearchConverter(this._connectionSettings, descriptor);
			var pathInfo = ((IPathInfo<MultiSearchQueryString>) descriptor).ToPathInfo(this._connectionSettings);
			var json = this.Serializer.SerializeMultiSearch(descriptor);
			return this.RawDispatch.MsearchDispatchAsync(pathInfo, json)
				.ContinueWith<IMultiSearchResponse>(t =>
				{
					var status = t.Result;
					var multiSearchResponse = this.Serializer.DeserializeInternal<MultiSearchResponse>(
						status,
						piggyBackJsonConverter: multiSearchConverter
					);
					return multiSearchResponse;
				});

		}
	}
}
