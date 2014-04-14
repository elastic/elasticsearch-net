using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISpanFirstQuery
	{
		[JsonProperty(PropertyName = "match")]
		ISpanQuery SpanQuery { get; set; }

		[JsonProperty(PropertyName = "end")]
		int? _End { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanFirstQueryDescriptor<T> : ISpanSubQuery, IQuery, ISpanFirstQuery where T : class
	{
		ISpanQuery ISpanFirstQuery.SpanQuery { get; set; }

		int? ISpanFirstQuery._End { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				var query = ((ISpanFirstQuery)this).SpanQuery as IQuery;
				return query != null && (((ISpanFirstQuery)this).SpanQuery == null || query.IsConditionless);
			}
		}

		public SpanFirstQueryDescriptor<T> MatchTerm(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var span = new SpanQuery<T>();
			span = span.SpanTerm(fieldDescriptor, value, Boost);
			((ISpanFirstQuery)this).SpanQuery = span;
			return this;
		}
		public SpanFirstQueryDescriptor<T> MatchTerm(string field, string value, double? Boost = null)
		{
			var span = new SpanQuery<T>();
			span = span.SpanTerm(field, value, Boost);
			((ISpanFirstQuery)this).SpanQuery = span;
			return this;
		}
		public SpanFirstQueryDescriptor<T> Match(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			selector.ThrowIfNull("selector");
			((ISpanFirstQuery)this).SpanQuery = selector(new SpanQuery<T>());
			return this;
		}
		public SpanFirstQueryDescriptor<T> End(int end)
		{
			((ISpanFirstQuery)this)._End = end;
			return this;
		}

	}
}
