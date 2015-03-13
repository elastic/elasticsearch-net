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
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Boosting = this;
		}

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public QueryContainer PositiveQuery { get; set; }
		public QueryContainer NegativeQuery { get; set; }
		public double? NegativeBoost { get; set; }
	}

	public class BoostingQueryDescriptor<T> : IBoostingQuery where T : class
	{
		private IBoostingQuery Self { get { return this; } }

		QueryContainer IBoostingQuery.PositiveQuery { get; set; }

		QueryContainer IBoostingQuery.NegativeQuery { get; set; }

		double? IBoostingQuery.NegativeBoost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (Self.NegativeQuery == null && Self.PositiveQuery == null)
					return true;
				return (Self.PositiveQuery == null || Self.NegativeQuery.IsConditionless)
					|| (Self.NegativeQuery == null || Self.PositiveQuery.IsConditionless);
			}
		}

		string IQuery.Name { get; set; }

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
