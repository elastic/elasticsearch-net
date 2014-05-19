using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;


namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<IndicesQueryDescriptor<object>>))]
	public interface IIndicesQuery : IQuery
	{
		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? Score { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryDescriptor Query { get; set; }

		[JsonProperty("no_match_query")]
		IQueryDescriptor NoMatchQuery { get; set; }

		[JsonProperty("indices")]
		IEnumerable<string> Indices { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class IndicesQueryDescriptor<T> : IIndicesQuery where T : class
	{
		NestedScore? IIndicesQuery.Score { get; set; }

		IQueryDescriptor IIndicesQuery.Query { get; set; }

		IQueryDescriptor IIndicesQuery.NoMatchQuery { get; set; }

		IEnumerable<string> IIndicesQuery.Indices { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IIndicesQuery)this).NoMatchQuery == null && ((IIndicesQuery)this).Query == null;
			}
		}

		public IndicesQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<T> { Descriptor = q };

			((IIndicesQuery)this).Query = d.Descriptor;
			return this;
		}

		public IndicesQueryDescriptor<T> Query<K>(Func<QueryDescriptor<K>, BaseQuery> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<K> { Descriptor = q };

			((IIndicesQuery)this).Query = d.Descriptor;
			return this;
		}
		
		public IndicesQueryDescriptor<T> NoMatchQuery(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<T> { Descriptor = q };

			((IIndicesQuery)this).NoMatchQuery = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery<K>(Func<QueryDescriptor<K>, IQueryDescriptor> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			var d = new RawOrQueryDescriptor<K> { Descriptor = q };

			((IIndicesQuery)this).NoMatchQuery = d.Descriptor;
			return this;
		}
		public IndicesQueryDescriptor<T> Indices(IEnumerable<string> indices)
		{
			((IIndicesQuery)this).Indices = indices;
			return this;
		}
	}
}
