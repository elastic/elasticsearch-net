using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<BoostingQueryDescriptor<object>>))]
	public interface IBoostingQuery : IQuery
	{
		[JsonProperty("positive")]
		BaseQuery PositiveQuery { get; set; }

		[JsonProperty("negative")]
		BaseQuery NegativeQuery { get; set; }

		[JsonProperty("negative_boost")]
		double? NegativeBoost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoostingQueryDescriptor<T> : IBoostingQuery where T : class
	{
		BaseQuery IBoostingQuery.PositiveQuery { get; set; }

		BaseQuery IBoostingQuery.NegativeQuery { get; set; }

		double? IBoostingQuery.NegativeBoost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (((IBoostingQuery)this).NegativeQuery == null && ((IBoostingQuery)this).PositiveQuery == null)
					return true;
				return ((IBoostingQuery)this).PositiveQuery == null && ((IBoostingQuery)this).NegativeQuery.IsConditionless
					|| ((IBoostingQuery)this).NegativeQuery == null && ((IBoostingQuery)this).PositiveQuery.IsConditionless;
			}
		}

		public BoostingQueryDescriptor<T> NegativeBoost(double boost)
		{
			((IBoostingQuery)this).NegativeBoost = boost;
			return this;
		}

		public BoostingQueryDescriptor<T> Positive(Func<QueryDescriptor<T>, BaseQuery> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			((IBoostingQuery)this).PositiveQuery = q;
			return this;
		}
		public BoostingQueryDescriptor<T> Negative(Func<QueryDescriptor<T>, BaseQuery> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			((IBoostingQuery)this).NegativeQuery = q;
			return this;
		}
	}
}
