using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;


namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDisMaxQuery : IQuery
	{
		[JsonProperty(PropertyName = "tie_breaker")]
		double? TieBreaker { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "queries")]
		IEnumerable<BaseQuery> Queries { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class DisMaxQueryDescriptor<T> : IDisMaxQuery where T : class
	{
		double? IDisMaxQuery.TieBreaker { get; set; }

		double? IDisMaxQuery.Boost { get; set; }

		IEnumerable<BaseQuery> IDisMaxQuery.Queries { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((IDisMaxQuery)this).Queries.HasAny() || ((IDisMaxQuery)this).Queries.All(q => q.IsConditionless);
			}
		}

		public DisMaxQueryDescriptor<T> Queries(params Func<QueryDescriptor<T>, BaseQuery>[] querySelectors)
		{
			var queries = new List<BaseQuery>();
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
