using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoostingQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty("positive")]
		internal BaseQuery _PositiveQuery { get; set; }

		[JsonProperty("negative")]
		internal BaseQuery _NegativeQuery { get; set; }

		[JsonProperty("negative_boost")]
		internal double? _NegativeBoost { get; set; }

		public bool IsConditionless
		{
			get
			{
				if (this._NegativeQuery == null && this._PositiveQuery == null)
					return true;
				return this._PositiveQuery == null && this._NegativeQuery.IsConditionlessQueryDescriptor
					|| this._NegativeQuery == null && this._PositiveQuery.IsConditionlessQueryDescriptor;
			}
		}

		public BoostingQueryDescriptor<T> NegativeBoost(double boost)
		{
			this._NegativeBoost = boost;
			return this;
		}

		public BoostingQueryDescriptor<T> Positive(Func<QueryDescriptor<T>, BaseQuery> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			this._PositiveQuery = q;
			return this;
		}
		public BoostingQueryDescriptor<T> Negative(Func<QueryDescriptor<T>, BaseQuery> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			this._NegativeQuery = q;
			return this;
		}
	}
}
