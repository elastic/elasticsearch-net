using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class FilteredQueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal QueryDescriptor<T> _Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		internal FilterDescriptor<T> _Filter { get; set; }

		public FilteredQueryDescriptor<T> Query(Action<QueryDescriptor<T>> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			querySelector(query);

			this._Query = query;
			return this;
		}

		public FilteredQueryDescriptor<T> Filter(Action<FilterDescriptor<T>> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			filterSelector(filter);

			this._Filter = filter;
			return this;
		}
	}
}
