using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public class ConditionlessFilterDescriptor<T> where T : class
	{
		internal BaseFilter _Filter { get; set; }

		internal BaseFilter _Fallback { get; set; }

		internal bool IsConditionless
		{
			get
			{
				return (this._Filter == null || this._Filter.IsConditionless)
					   && (this._Fallback == null || this._Fallback.IsConditionless);

			}
		}

		public ConditionlessFilterDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this._Filter = f;
			return this;
		}

		public ConditionlessFilterDescriptor<T> Fallback(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this._Fallback = f;
			return this;
		}
	}
}

