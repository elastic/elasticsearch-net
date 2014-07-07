using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<BoostingQueryDescriptor<object>>))]
	public interface IBoostingQuery : IQuery
	{
		[JsonProperty("positive")]
		QueryContainer PositiveQuery { get; set; }

		[JsonProperty("negative")]
		QueryContainer NegativeQuery { get; set; }

		[JsonProperty("negative_boost")]
		double? NegativeBoost { get; set; }
	}

	public class BoostingQuery : PlainQuery, IBoostingQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Boosting = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public QueryContainer PositiveQuery { get; set; }
		public QueryContainer NegativeQuery { get; set; }
		public double? NegativeBoost { get; set; }
	}

	public class BoostingQueryDescriptor<T> : IBoostingQuery where T : class
	{
		QueryContainer IBoostingQuery.PositiveQuery { get; set; }

		QueryContainer IBoostingQuery.NegativeQuery { get; set; }

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

		public BoostingQueryDescriptor<T> Positive(Func<QueryDescriptor<T>, QueryContainer> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			((IBoostingQuery)this).PositiveQuery = q;
			return this;
		}
		public BoostingQueryDescriptor<T> Negative(Func<QueryDescriptor<T>, QueryContainer> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			((IBoostingQuery)this).NegativeQuery = q;
			return this;
		}
	}
}
