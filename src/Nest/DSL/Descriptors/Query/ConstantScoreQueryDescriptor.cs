using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ConstantScoreQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal BaseQuery _Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		internal BaseFilter _Filter { get; set; }

		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }

		internal bool IsConditionless
		{
			get
			{
				if (this._Query == null && this._Filter == null)
					return true;
				else if (this._Filter == null && this._Query != null)
					return this._Query.IsConditionlessQueryDescriptor;
				//TODO FILTER
				return false;
			}
		}

		public ConstantScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			this._Filter = null;
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			this._Query = q;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			this._Query = null;
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this._Filter = f;
			return this;
		}

		public ConstantScoreQueryDescriptor<T> Boost(double boost)
		{
			boost.ThrowIfNull("boostFactor");
			this._Boost = boost;
			return this;
		}
	}
}
