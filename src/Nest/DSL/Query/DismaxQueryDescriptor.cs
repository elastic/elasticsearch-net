using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<DisMaxQueryDescriptor<object>>))]
	public interface IDisMaxQuery : IQuery
	{
		[JsonProperty(PropertyName = "tie_breaker")]
		double? TieBreaker { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "queries")]
		IEnumerable<QueryContainer> Queries { get; set; }
	}

	public class DismaxQuery : PlainQuery, IDisMaxQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.DisMax = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public double? TieBreaker { get; set; }
		public double? Boost { get; set; }
		public IEnumerable<QueryContainer> Queries { get; set; }
	}

	public class DisMaxQueryDescriptor<T> : IDisMaxQuery where T : class
	{
		double? IDisMaxQuery.TieBreaker { get; set; }

		double? IDisMaxQuery.Boost { get; set; }

		IEnumerable<QueryContainer> IDisMaxQuery.Queries { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((IDisMaxQuery)this).Queries.HasAny() || ((IDisMaxQuery)this).Queries.All(q => q.IsConditionless);
			}
		}

		public DisMaxQueryDescriptor<T> Queries(params Func<QueryDescriptor<T>, QueryContainer>[] querySelectors)
		{
			var queries = new List<QueryContainer>();
			foreach (var selector in querySelectors)
			{
				var query = new QueryDescriptor<T>();
				var q = selector(query);
				queries.Add(q);
			}
			((IDisMaxQuery)this).Queries = queries;
			return this;
		}

		public DisMaxQueryDescriptor<T> Boost(double boost)
		{
			((IDisMaxQuery)this).Boost = boost;
			return this;
		}
		public DisMaxQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			((IDisMaxQuery)this).TieBreaker = tieBreaker;
			return this;
		}
	}
}
