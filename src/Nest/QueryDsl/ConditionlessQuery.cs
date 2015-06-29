
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class ConditionlessQueryDescriptor<T> : IQuery where T : class
	{
		internal QueryContainer _Query { get; set; }

		internal QueryContainer _Fallback { get; set; }

		bool IQuery.Conditionless
		{
			get
			{
				return (this._Query == null || this._Query.IsConditionless)
					   && (this._Fallback == null || this._Fallback.IsConditionless);

			}
		}

		string IQuery.Name { get; set; }

		public ConditionlessQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			this._Query = q;
			return this;
		}

		public ConditionlessQueryDescriptor<T> Fallback(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			this._Fallback = q;
			return this;
		}
	}
}
