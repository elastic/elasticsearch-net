using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
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
		IQueryContainer Query { get; set; }

		[JsonProperty("no_match_query")]
		IQueryContainer NoMatchQuery { get; set; }

		[JsonProperty("indices")]
		IEnumerable<string> Indices { get; set; }
	}

	public class IndicesQuery : PlainQuery, IIndicesQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Indices = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public NestedScore? Score { get; set; }
		public IQueryContainer Query { get; set; }
		public IQueryContainer NoMatchQuery { get; set; }
		public IEnumerable<string> Indices { get; set; }
	}

	public class IndicesQueryDescriptor<T> : IIndicesQuery where T : class
	{
		NestedScore? IIndicesQuery.Score { get; set; }

		IQueryContainer IIndicesQuery.Query { get; set; }

		IQueryContainer IIndicesQuery.NoMatchQuery { get; set; }

		IEnumerable<string> IIndicesQuery.Indices { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IIndicesQuery)this).NoMatchQuery == null && ((IIndicesQuery)this).Query == null;
			}
		}

		public IndicesQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;


			((IIndicesQuery)this).Query = q;
			return this;
		}

		public IndicesQueryDescriptor<T> Query<K>(Func<QueryDescriptor<K>, QueryContainer> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			((IIndicesQuery)this).Query = q;
			return this;
		}
		
		public IndicesQueryDescriptor<T> NoMatchQuery(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			((IIndicesQuery)this).NoMatchQuery = q;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery<K>(Func<QueryDescriptor<K>, IQueryContainer> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			((IIndicesQuery)this).NoMatchQuery = q;
			return this;
		}
		public IndicesQueryDescriptor<T> Indices(IEnumerable<string> indices)
		{
			((IIndicesQuery)this).Indices = indices;
			return this;
		}
	}
}
