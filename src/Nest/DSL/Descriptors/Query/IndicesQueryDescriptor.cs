using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;


namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
  public class IndicesQueryDescriptor<T>  where T : class
	{
		[JsonProperty("score_mode"), JsonConverter(typeof(StringEnumConverter))]
		internal NestedScore? _Score { get; set; }

		[JsonProperty("query")]
		internal object _QueryDescriptor { get; set; }

		[JsonProperty("no_match_query")]
		internal object _NoMatchQueryDescriptor { get; set; }

		[JsonProperty("indices")]
		internal IEnumerable<string> _Indices { get; set; }

		public IndicesQueryDescriptor<T> Query(Action<QueryDescriptor<T>> querySelector)
		{
      var qd = new QueryDescriptor<T>();
			var d = new RawOrQueryDescriptor<T> { Descriptor = qd };
      querySelector(qd);
			this._QueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> Query<K>(Action<QueryDescriptor<K>> querySelector) where K : class
		{
      var qd = new QueryDescriptor<K>();
      var d = new RawOrQueryDescriptor<K> { Descriptor = qd };
      querySelector(qd);
      this._QueryDescriptor = qd;
			return this;
		}
		public IndicesQueryDescriptor<T> Query(string rawQuery)
		{
			var d = new RawOrQueryDescriptor<T> { Raw = rawQuery };
			this._QueryDescriptor = d;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery(Action<QueryDescriptor<T>> querySelector)
		{
      var qd = new QueryDescriptor<T>();
			var d = new RawOrQueryDescriptor<T> { Descriptor = qd };
			querySelector(qd);
			this._NoMatchQueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery<K>(Action<QueryDescriptor<K>> querySelector) where K : class
		{
      var qd = new QueryDescriptor<K>();
			var d = new RawOrQueryDescriptor<K> { Descriptor = qd  };
			querySelector(qd);
			this._NoMatchQueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery(string rawQuery)
		{
			var d = new RawOrQueryDescriptor<T> { Raw = rawQuery };
			this._QueryDescriptor = d;
			return this;
		}
		public IndicesQueryDescriptor<T> Indices(IEnumerable<string> indices)
		{
			this._Indices = indices;
			return this;
		}
	}
}
