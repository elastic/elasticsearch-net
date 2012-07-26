using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace Nest
{
  public class DismaxQueryDescriptor<T>  where T : class
	{
		[JsonProperty(PropertyName = "tie_breaker")]
		internal double? _TieBreaker { get; set; }

		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }

		[JsonProperty(PropertyName = "queries")]
		internal IEnumerable<QueryDescriptor<T>> _Queries { get; set; }

    public DismaxQueryDescriptor<T> Queries(params Func<QueryDescriptor<T>, BaseQuery>[] querySelectors)
		{
			var queries = new List<QueryDescriptor<T>>();
			foreach (var selector in querySelectors)
			{
				var query = new QueryDescriptor<T>();
				selector(query);
				queries.Add(query);
			}
			this._Queries = queries;
			return this;
		}

		public DismaxQueryDescriptor<T> Boost(double boost)
		{
			boost.ThrowIfNull("boost");
			this._Boost = boost;
			return this;
		}
		public DismaxQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			tieBreaker.ThrowIfNull("tieBreaker");
			this._TieBreaker = tieBreaker;
			return this;
		}
	}
}
