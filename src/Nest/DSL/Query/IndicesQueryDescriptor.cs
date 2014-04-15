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
	public interface IIndicesQuery
	{
		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? Score { get; set; }

		[JsonProperty("query")]
		object QueryDescriptor { get; set; }

		[JsonProperty("no_match_query")]
		object NoMatchQueryDescriptor { get; set; }

		[JsonProperty("indices")]
		IEnumerable<string> Indices { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class IndicesQueryDescriptor<T> : IQuery, IIndicesQuery where T : class
	{
		NestedScore? IIndicesQuery.Score { get; set; }

		object IIndicesQuery.QueryDescriptor { get; set; }

		object IIndicesQuery.NoMatchQueryDescriptor { get; set; }

		IEnumerable<string> IIndicesQuery.Indices { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IIndicesQuery)this).NoMatchQueryDescriptor == null && ((IIndicesQuery)this).QueryDescriptor == null;
			}
		}


		public IndicesQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<T> { Descriptor = q };

			((IIndicesQuery)this).QueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> Query<K>(Func<QueryDescriptor<K>, BaseQuery> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<K> { Descriptor = q };

			((IIndicesQuery)this).QueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> Query(string rawQuery)
		{
			var d = new RawOrQueryDescriptor<T> { Raw = rawQuery };
			((IIndicesQuery)this).QueryDescriptor = d;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<T> { Descriptor = q };

			((IIndicesQuery)this).NoMatchQueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery<K>(Func<QueryDescriptor<K>, IQueryDescriptor> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<K> { Descriptor = q };

			((IIndicesQuery)this).NoMatchQueryDescriptor = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery(string rawQuery)
		{
			var d = new RawOrQueryDescriptor<T> { Raw = rawQuery };
			((IIndicesQuery)this).QueryDescriptor = d;
			return this;
		}
		public IndicesQueryDescriptor<T> Indices(IEnumerable<string> indices)
		{
			((IIndicesQuery)this).Indices = indices;
			return this;
		}
	}
}
