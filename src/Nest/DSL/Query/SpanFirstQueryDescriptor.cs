using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SpanFirstQueryDescriptor<object>>))]
	public interface ISpanFirstQuery : ISpanSubQuery
	{
		[JsonProperty(PropertyName = "match")]
		ISpanQuery Match { get; set; }

		[JsonProperty(PropertyName = "end")]
		int? End { get; set; }
	}

	public class SpanFirstQuery : PlainQuery, ISpanFirstQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanFirst = this;
		}

		bool IQuery.IsConditionless { get { return false; } }

		public ISpanQuery Match { get; set; }
		public int? End { get; set; }
	}

	public class SpanFirstQueryDescriptor<T> : ISpanFirstQuery where T : class
	{
		ISpanQuery ISpanFirstQuery.Match { get; set; }

		int? ISpanFirstQuery.End { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				var query = ((ISpanFirstQuery)this).Match as IQuery;
				return query != null && (((ISpanFirstQuery)this).Match == null || query.IsConditionless);
			}
		}

		public SpanFirstQueryDescriptor<T> MatchTerm(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var span = new SpanQuery<T>();
			span = span.SpanTerm(fieldDescriptor, value, Boost);
			((ISpanFirstQuery)this).Match = span;
			return this;
		}
		public SpanFirstQueryDescriptor<T> MatchTerm(string field, string value, double? Boost = null)
		{
			var span = new SpanQuery<T>();
			span = span.SpanTerm(field, value, Boost);
			((ISpanFirstQuery)this).Match = span;
			return this;
		}
		public SpanFirstQueryDescriptor<T> Match(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			selector.ThrowIfNull("selector");
			((ISpanFirstQuery)this).Match = selector(new SpanQuery<T>());
			return this;
		}
		public SpanFirstQueryDescriptor<T> End(int end)
		{
			((ISpanFirstQuery)this).End = end;
			return this;
		}

	}
}
