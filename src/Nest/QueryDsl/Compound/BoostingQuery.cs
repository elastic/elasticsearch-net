using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
		public string Name { get; set; }
		bool IQuery.IsConditionless => IsConditionless(this);
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

	public class BoostingQueryDescriptor<T> : IBoostingQuery where T : class
	{
		private IBoostingQuery Self => this;
		bool IQuery.IsConditionless => BoostingQuery.IsConditionless(this);
		string IQuery.Name { get; set; }
		QueryContainer IBoostingQuery.PositiveQuery { get; set; }
		QueryContainer IBoostingQuery.NegativeQuery { get; set; }
		double? IBoostingQuery.NegativeBoost { get; set; }

		public BoostingQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public BoostingQueryDescriptor<T> NegativeBoost(double boost)
		{
			Self.NegativeBoost = boost;
			return this;
		}

		public BoostingQueryDescriptor<T> Positive(Func<QueryDescriptor<T>, QueryContainer> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			Self.PositiveQuery = q;
			return this;
		}

		public BoostingQueryDescriptor<T> Negative(Func<QueryDescriptor<T>, QueryContainer> selector)
		{
			var query = new QueryDescriptor<T>();
			var q = selector(query);
			Self.NegativeQuery = q;
			return this;
		}
	}
}
