using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFilteredQuery
	{
		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor _Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		BaseFilter _Filter { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FilteredQueryDescriptor<T> : IQuery, IFilteredQuery where T : class
	{
		IQueryDescriptor IFilteredQuery._Query { get; set; }

		BaseFilter IFilteredQuery._Filter { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (((IFilteredQuery)this)._Query == null && ((IFilteredQuery)this)._Filter == null)
					return true;
				if (((IFilteredQuery)this)._Filter == null && ((IFilteredQuery)this)._Query != null)
					return ((IFilteredQuery)this)._Query.IsConditionless;
				if (((IFilteredQuery)this)._Filter != null && ((IFilteredQuery)this)._Query == null)
					return ((IFilteredQuery)this)._Filter.IsConditionless;
				return ((IFilteredQuery)this)._Query.IsConditionless && ((IFilteredQuery)this)._Filter.IsConditionless;
			}
		}

		public FilteredQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((IFilteredQuery)this)._Query = q;
			return this;
		}

		public FilteredQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			((IFilteredQuery)this)._Filter = f;
			return this;
		}
	}
}
