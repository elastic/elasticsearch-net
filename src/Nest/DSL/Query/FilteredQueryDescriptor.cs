using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FilteredQueryDescriptor<object>>))]
	public interface IFilteredQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		BaseFilterDescriptor Filter { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FilteredQueryDescriptor<T> : IFilteredQuery where T : class
	{
		IQueryDescriptor IFilteredQuery.Query { get; set; }

		BaseFilterDescriptor IFilteredQuery.Filter { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (((IFilteredQuery)this).Query == null && ((IFilteredQuery)this).Filter == null)
					return true;
				if (((IFilteredQuery)this).Filter == null && ((IFilteredQuery)this).Query != null)
					return ((IFilteredQuery)this).Query.IsConditionless;
				if (((IFilteredQuery)this).Filter != null && ((IFilteredQuery)this).Query == null)
					return ((IFilteredQuery)this).Filter.IsConditionless;
				return ((IFilteredQuery)this).Query.IsConditionless && ((IFilteredQuery)this).Filter.IsConditionless;
			}
		}

		public FilteredQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((IFilteredQuery)this).Query = q;
			return this;
		}

		public FilteredQueryDescriptor<T> Filter(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptorDescriptor<T>();
			var f = filterSelector(filter);

			((IFilteredQuery)this).Filter = f;
			return this;
		}
	}
}
