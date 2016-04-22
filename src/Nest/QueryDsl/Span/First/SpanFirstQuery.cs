using System;
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
		protected override bool Conditionless => IsConditionless(this);
		public ISpanQuery Match { get; set; }
		public int? End { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanFirst = this;
		internal static bool IsConditionless(ISpanFirstQuery q) => q.Match == null || q.Match.Conditionless;
	}

	public class SpanFirstQueryDescriptor<T> 
		: QueryDescriptorBase<SpanFirstQueryDescriptor<T>, ISpanFirstQuery>
		, ISpanFirstQuery where T : class
	{
		protected override bool Conditionless => SpanFirstQuery.IsConditionless(this);	
		ISpanQuery ISpanFirstQuery.Match { get; set; }
		int? ISpanFirstQuery.End { get; set; }

		public SpanFirstQueryDescriptor<T> Match(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(a => a.Match = selector(new SpanQueryDescriptor<T>()));

		public SpanFirstQueryDescriptor<T> End(int? end) => Assign(a => a.End = end);
	}
}
