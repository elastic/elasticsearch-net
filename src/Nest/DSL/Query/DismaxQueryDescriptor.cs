using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;


namespace Nest
{
	public interface IDismaxQuery
	{
		[JsonProperty(PropertyName = "tie_breaker")]
		double? _TieBreaker { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "queries")]
		IEnumerable<BaseQuery> _Queries { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class DismaxQueryDescriptor<T> : IQuery, IDismaxQuery where T : class
	{
		[JsonProperty(PropertyName = "tie_breaker")]
		double? IDismaxQuery._TieBreaker { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? IDismaxQuery._Boost { get; set; }

		[JsonProperty(PropertyName = "queries")]
		IEnumerable<BaseQuery> IDismaxQuery._Queries { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((IDismaxQuery)this)._Queries.HasAny() || ((IDismaxQuery)this)._Queries.All(q => q.IsConditionless);
			}
		}

		public DismaxQueryDescriptor<T> Queries(params Func<QueryDescriptor<T>, BaseQuery>[] querySelectors)
		{
			var queries = new List<BaseQuery>();
			foreach (var selector in querySelectors)
			{
				var query = new QueryDescriptor<T>();
				var q = selector(query);
				queries.Add(q);
			}
			((IDismaxQuery)this)._Queries = queries;
			return this;
		}

		public DismaxQueryDescriptor<T> Boost(double boost)
		{
			((IDismaxQuery)this)._Boost = boost;
			return this;
		}
		public DismaxQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			((IDismaxQuery)this)._TieBreaker = tieBreaker;
			return this;
		}
	}
}
