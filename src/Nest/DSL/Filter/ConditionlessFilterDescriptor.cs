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
		internal BaseFilterDescriptor FilterDescriptor { get; set; }

		internal BaseFilterDescriptor _Fallback { get; set; }

		internal bool IsConditionless
		{
			get
			{
				return (this.FilterDescriptor == null || this.FilterDescriptor.IsConditionless)
					   && (this._Fallback == null || this._Fallback.IsConditionless);

			}
		}

		public ConditionlessFilterDescriptor<T> Filter(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptorDescriptor<T>();
			var f = filterSelector(filter);

			this.FilterDescriptor = f;
			return this;
		}

		public ConditionlessFilterDescriptor<T> Fallback(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptorDescriptor<T>();
			var f = filterSelector(filter);

			this._Fallback = f;
			return this;
		}
	}
}

