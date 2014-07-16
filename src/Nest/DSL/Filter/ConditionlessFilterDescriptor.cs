using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class ConditionlessFilterDescriptor<T> where T : class
	{
		internal FilterContainer FilterDescriptor { get; set; }

		internal FilterContainer _Fallback { get; set; }

		internal bool IsConditionless
		{
			get
			{
				return (this.FilterDescriptor == null || this.FilterDescriptor.IsConditionless)
					   && (this._Fallback == null || this._Fallback.IsConditionless);

			}
		}

		public ConditionlessFilterDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this.FilterDescriptor = f;
			return this;
		}

		public ConditionlessFilterDescriptor<T> Fallback(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this._Fallback = f;
			return this;
		}
	}
}

