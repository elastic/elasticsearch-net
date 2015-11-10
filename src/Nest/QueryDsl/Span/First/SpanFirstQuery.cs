using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SpanFirstQueryDescriptor<object>>))]
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
		bool IQuery.Conditionless => SpanFirstQuery.IsConditionless(this);	
		ISpanQuery ISpanFirstQuery.Match { get; set; }
		int? ISpanFirstQuery.End { get; set; }

		public SpanFirstQueryDescriptor<T> Match(Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>> selector) =>
			Assign(a => a.Match = selector(new SpanQueryDescriptor<T>()));

		public SpanFirstQueryDescriptor<T> End(int? end) => Assign(a => a.End = end);
	}
}
