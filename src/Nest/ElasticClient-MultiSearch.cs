using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.Resolvers;
using System.Reflection;
using System.Collections.Concurrent;

namespace Nest
{
	public partial class ElasticClient
	{
		public MultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector)
		{
			multiSearchSelector.ThrowIfNull("multiSearchSelector");
			var multiSearchDescriptor = multiSearchSelector(new MultiSearchDescriptor());
			return this.MultiSearch(multiSearchDescriptor);
		}

		public MultiSearchResponse MultiSearch(MultiSearchDescriptor multiSearchDescriptor)
		{
			multiSearchDescriptor.ThrowIfNull("multiSearchDescriptor");
			var sb = new StringBuilder();

			foreach (var operation in multiSearchDescriptor._Operations.Values)
			{
				var indeces = operation._Indices.HasAny() ? string.Join(",", operation._Indices) : null;
				if (operation._AllIndices)
					indeces = "_all";
				
				var index = indeces ??
							multiSearchDescriptor._FixedIndex ??
				            new IndexNameResolver(this._connectionSettings).GetIndexForType(operation._ClrType);
				
				var types =  operation._Types.HasAny() ? string.Join(",", operation._Types.Select(x => x.Resolve(this._connectionSettings)) ) : null;

				var typeName = types
							   ?? multiSearchDescriptor._FixedType
				               ?? TypeNameMarker.Create(operation._ClrType);
				if (operation._AllTypes)
					typeName = null; //force empty typename so we'll query all types.

				var op = new { index = index, type = typeName, search_type = this.GetSearchType(operation), preference = operation._Preference, routing = operation._Routing };
				var opJson = this.Serializer.Serialize(op, Formatting.None);

				var action = "{0}\n".F(opJson);
				sb.Append(action);
				var searchJson = this.Serializer.Serialize(operation, Formatting.None);
				sb.Append(searchJson + "\n");

			}
			var json = sb.ToString();
			var path = "_msearch";
			if (!multiSearchDescriptor._FixedIndex.IsNullOrEmpty())
			{
				if (!multiSearchDescriptor._FixedType.IsNullOrEmpty())
					path = multiSearchDescriptor._FixedType + "/" + path;
				path = multiSearchDescriptor._FixedIndex + "/" + path;
			}
			var status = this.Connection.PostSync(path, json);

			var multiSearchConverter = new MultiSearchConverter(this._connectionSettings, multiSearchDescriptor);
			var multiSearchResponse = this.Serializer.DeserializeInternal<MultiSearchResponse>(
				status, 
				piggyBackJsonConverter: multiSearchConverter
			);

			return multiSearchResponse;
		}

		private string GetSearchType(SearchDescriptorBase descriptor)
		{
			if (!descriptor._SearchType.HasValue)
				return null;
			switch (descriptor._SearchType.Value)
			{
				case SearchType.Count:
					return "count";
				case SearchType.DfsQueryThenFetch:
					return "dfs_query_then_fetch";
				case SearchType.DfsQueryAndFetch:
					return "dfs_query_and_fetch";
				case SearchType.QueryThenFetch:
					return "query_then_fetch";
				case SearchType.QueryAndFetch:
					return "query_and_fetch";
				case SearchType.Scan:
					return "scan";
			}
			return null;
		}
	}
}
