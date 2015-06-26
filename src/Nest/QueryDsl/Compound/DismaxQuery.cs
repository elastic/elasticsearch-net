using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.Resolvers.Converters.Queries;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(DismaxQueryJsonConverter))]
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
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public double? TieBreaker { get; set; }
		public double? Boost { get; set; }
		public IEnumerable<QueryContainer> Queries { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.DisMax = this;
		}
	}

	public class DisMaxQueryDescriptor<T> : IDisMaxQuery where T : class
	{
		private IDisMaxQuery Self { get { return this; } }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				return !Self.Queries.HasAny() || Self.Queries.All(q => q.IsConditionless);
			}
		}
		double? IDisMaxQuery.TieBreaker { get; set; }
		double? IDisMaxQuery.Boost { get; set; }
		IEnumerable<QueryContainer> IDisMaxQuery.Queries { get; set; }

		public DisMaxQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
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
			Self.Queries = queries;
			return this;
		}

		public DisMaxQueryDescriptor<T> Queries(params QueryContainer[] queries)
		{
			var descriptors = new List<QueryContainer>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			((IDisMaxQuery)this).Queries = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		public DisMaxQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}

		public DisMaxQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			Self.TieBreaker = tieBreaker;
			return this;
		}
	}
}
