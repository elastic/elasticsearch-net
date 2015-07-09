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

	public class SpanFirstQuery : QueryBase, ISpanFirstQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public ISpanQuery Match { get; set; }
		public int? End { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.SpanFirst = this;
		internal static bool IsConditionless(ISpanFirstQuery q) => q.Match == null || q.Match.Conditionless;
	}

	public class SpanFirstQueryDescriptor<T> 
		: QueryDescriptorBase<SpanFirstQueryDescriptor<T>, ISpanFirstQuery>
		, ISpanFirstQuery where T : class
	{
		private ISpanFirstQuery Self { get { return this; }}
		bool IQuery.Conditionless => SpanFirstQuery.IsConditionless(this);	
		ISpanQuery ISpanFirstQuery.Match { get; set; }
		int? ISpanFirstQuery.End { get; set; }

		public SpanFirstQueryDescriptor<T> MatchTerm(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var span = new SpanQueryDescriptor<T>();
			span = span.SpanTerm(fieldDescriptor, value, Boost);
			Self.Match = span;
			return this;
		}

		public SpanFirstQueryDescriptor<T> MatchTerm(string field, string value, double? Boost = null)
		{
			var span = new SpanQueryDescriptor<T>();
			span = span.SpanTerm(field, value, Boost);
			Self.Match = span;
			return this;
		}

		public SpanFirstQueryDescriptor<T> Match(Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			Self.Match = selector(new SpanQueryDescriptor<T>());
			return this;
		}

		public SpanFirstQueryDescriptor<T> End(int end)
		{
			Self.End = end;
			return this;
		}
	}
}
