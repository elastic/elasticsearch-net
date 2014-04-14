using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	public interface IBoostingQuery
	{
		[JsonProperty("positive")]
		BaseQuery _PositiveQuery { get; set; }

		[JsonProperty("negative")]
		BaseQuery _NegativeQuery { get; set; }

		[JsonProperty("negative_boost")]
		double? _NegativeBoost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoostingQueryDescriptor<T> : IQuery, IBoostingQuery where T : class
	{
		[JsonProperty("positive")]
		BaseQuery IBoostingQuery._PositiveQuery { get; set; }

		[JsonProperty("negative")]
		BaseQuery IBoostingQuery._NegativeQuery { get; set; }

		[JsonProperty("negative_boost")]
		double? IBoostingQuery._NegativeBoost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (((IBoostingQuery)this)._NegativeQuery == null && ((IBoostingQuery)this)._PositiveQuery == null)
					return true;
				return ((IBoostingQuery)this)._PositiveQuery == null && ((IBoostingQuery)this)._NegativeQuery.IsConditionless
					|| ((IBoostingQuery)this)._NegativeQuery == null && ((IBoostingQuery)this)._PositiveQuery.IsConditionless;
			}
		}

		public BoostingQueryDescriptor<T> NegativeBoost(double boost)
		{
			((IBoostingQuery)this)._NegativeBoost = boost;
			return this;
		}

		public BoostingQueryDescriptor<T> Positive(Func<QueryDescriptor<T>, BaseQuery> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			((IBoostingQuery)this)._PositiveQuery = q;
			return this;
		}
		public BoostingQueryDescriptor<T> Negative(Func<QueryDescriptor<T>, BaseQuery> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			((IBoostingQuery)this)._NegativeQuery = q;
			return this;
		}
	}
}
