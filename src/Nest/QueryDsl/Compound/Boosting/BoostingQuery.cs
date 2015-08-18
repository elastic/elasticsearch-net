using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BoostingQueryDescriptor<object>>))]
	public interface IBoostingQuery : IQuery
	{
		[JsonProperty("positive")]
		QueryContainer PositiveQuery { get; set; }

		[JsonProperty("negative")]
		QueryContainer NegativeQuery { get; set; }

		[JsonProperty("negative_boost")]
		double? NegativeBoost { get; set; }
	}

	public class BoostingQuery : QueryBase, IBoostingQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public QueryContainer PositiveQuery { get; set; }
		public QueryContainer NegativeQuery { get; set; }
		public double? NegativeBoost { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Boosting = this;

		internal static bool IsConditionless(IBoostingQuery q)
		{
			if (q.NegativeQuery == null && q.PositiveQuery == null)
				return true;

			return (q.PositiveQuery == null && q.NegativeQuery.IsConditionless)
				|| (q.NegativeQuery == null && q.PositiveQuery.IsConditionless);
		}
	}

	public class BoostingQueryDescriptor<T> 
		: QueryDescriptorBase<BoostingQueryDescriptor<T>, IBoostingQuery>
		, IBoostingQuery where T : class
	{
		bool IQuery.Conditionless => BoostingQuery.IsConditionless(this);
		QueryContainer IBoostingQuery.PositiveQuery { get; set; }
		QueryContainer IBoostingQuery.NegativeQuery { get; set; }
		double? IBoostingQuery.NegativeBoost { get; set; }

		public BoostingQueryDescriptor<T> NegativeBoost(double boost) => Assign(a => a.NegativeBoost = boost);

		public BoostingQueryDescriptor<T> Positive(Func<QueryContainerDescriptor<T>, QueryContainer> selector) 
			=> Assign(a => a.PositiveQuery = selector(new QueryContainerDescriptor<T>()));

		public BoostingQueryDescriptor<T> Negative(Func<QueryContainerDescriptor<T>, QueryContainer> selector) 
			=> Assign(a => a.NegativeQuery = selector(new QueryContainerDescriptor<T>()));
	}
}
