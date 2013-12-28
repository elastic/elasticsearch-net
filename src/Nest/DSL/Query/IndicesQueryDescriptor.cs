using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;


namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class IndicesQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty("score_mode"), JsonConverter(typeof(StringEnumConverter))]
		internal NestedScore? _Score { get; set; }

		[JsonProperty("query")]
		internal object _QueryDescriptor { get; set; }

		[JsonProperty("no_match_query")]
		internal object _NoMatchQueryDescriptor { get; set; }

		[JsonProperty("indices")]
		internal IEnumerable<string> _Indices { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return (this._NoMatchQueryDescriptor == null && this._QueryDescriptor == null);
			}
		}


		public IndicesQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<T> { Descriptor = q };

			this._QueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> Query<K>(Func<QueryDescriptor<K>, BaseQuery> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<K> { Descriptor = q };

			this._QueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> Query(string rawQuery)
		{
			var d = new RawOrQueryDescriptor<T> { Raw = rawQuery };
			this._QueryDescriptor = d;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<T> { Descriptor = q };

			this._NoMatchQueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery<K>(Func<QueryDescriptor<K>, BaseQuery> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<K> { Descriptor = q };

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
