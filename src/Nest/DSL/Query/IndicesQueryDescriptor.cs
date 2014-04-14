using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;


namespace Nest
{
	public interface IIndicesQuery
	{
		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? _Score { get; set; }

		[JsonProperty("query")]
		object _QueryDescriptor { get; set; }

		[JsonProperty("no_match_query")]
		object _NoMatchQueryDescriptor { get; set; }

		[JsonProperty("indices")]
		IEnumerable<string> _Indices { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class IndicesQueryDescriptor<T> : IQuery, IIndicesQuery where T : class
	{
		[JsonProperty("score_mode"), JsonConverter(typeof(StringEnumConverter))]
		NestedScore? IIndicesQuery._Score { get; set; }

		[JsonProperty("query")]
		object IIndicesQuery._QueryDescriptor { get; set; }

		[JsonProperty("no_match_query")]
		object IIndicesQuery._NoMatchQueryDescriptor { get; set; }

		[JsonProperty("indices")]
		IEnumerable<string> IIndicesQuery._Indices { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IIndicesQuery)this)._NoMatchQueryDescriptor == null && ((IIndicesQuery)this)._QueryDescriptor == null;
			}
		}


		public IndicesQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<T> { Descriptor = q };

			((IIndicesQuery)this)._QueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> Query<K>(Func<QueryDescriptor<K>, BaseQuery> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<K> { Descriptor = q };

			((IIndicesQuery)this)._QueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> Query(string rawQuery)
		{
			var d = new RawOrQueryDescriptor<T> { Raw = rawQuery };
			((IIndicesQuery)this)._QueryDescriptor = d;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<T> { Descriptor = q };

			((IIndicesQuery)this)._NoMatchQueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery<K>(Func<QueryDescriptor<K>, BaseQuery> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<K> { Descriptor = q };

			((IIndicesQuery)this)._NoMatchQueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery(string rawQuery)
		{
			var d = new RawOrQueryDescriptor<T> { Raw = rawQuery };
			((IIndicesQuery)this)._QueryDescriptor = d;
			return this;
		}
		public IndicesQueryDescriptor<T> Indices(IEnumerable<string> indices)
		{
			((IIndicesQuery)this)._Indices = indices;
			return this;
		}
	}
}
