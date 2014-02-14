using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FilteredQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal BaseQuery _Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		internal BaseFilter _Filter { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (this._Query == null && this._Filter == null)
					return true;
				if (this._Filter == null && this._Query != null)
					return this._Query.IsConditionless;
				if (this._Filter != null && this._Query == null)
					return this._Filter.IsConditionless;
				return this._Query.IsConditionless && this._Filter.IsConditionless;
			}
		}

		public FilteredQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			this._Query = q;
			return this;
		}

		public FilteredQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this._Filter = f;
			return this;
		}
	}
}
