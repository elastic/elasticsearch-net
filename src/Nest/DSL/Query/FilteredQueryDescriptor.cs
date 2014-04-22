using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFilteredQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		BaseFilterDescriptor FilterDescriptor { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FilteredQueryDescriptor<T> : IFilteredQuery where T : class
	{
		IQueryDescriptor IFilteredQuery.Query { get; set; }

		BaseFilterDescriptor IFilteredQuery.FilterDescriptor { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (((IFilteredQuery)this).Query == null && ((IFilteredQuery)this).FilterDescriptor == null)
					return true;
				if (((IFilteredQuery)this).FilterDescriptor == null && ((IFilteredQuery)this).Query != null)
					return ((IFilteredQuery)this).Query.IsConditionless;
				if (((IFilteredQuery)this).FilterDescriptor != null && ((IFilteredQuery)this).Query == null)
					return ((IFilteredQuery)this).FilterDescriptor.IsConditionless;
				return ((IFilteredQuery)this).Query.IsConditionless && ((IFilteredQuery)this).FilterDescriptor.IsConditionless;
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

			((IFilteredQuery)this).FilterDescriptor = f;
			return this;
		}
	}
}
