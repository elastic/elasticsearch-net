using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Elasticsearch.Net;


namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class DismaxQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "tie_breaker")]
		internal double? _TieBreaker { get; set; }

		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }

		[JsonProperty(PropertyName = "queries")]
		internal IEnumerable<BaseQuery> _Queries { get; set; }

		public bool IsStrict { get; private set; }
		public DismaxQueryDescriptor<T> Strict(bool strict = true) { this.IsStrict = strict; return this; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !this._Queries.HasAny() || this._Queries.All(q => q.IsConditionless);
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
			this._Queries = queries;
			return this;
		}

		public DismaxQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public DismaxQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			this._TieBreaker = tieBreaker;
			return this;
		}
	}
}
