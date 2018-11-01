using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SpanWithinQuery>))]
	public interface ISpanWithinQuery : ISpanSubQuery
	{
		[JsonProperty("big")]
		ISpanQuery Big { get; set; }

		[JsonProperty("little")]
		ISpanQuery Little { get; set; }
	}

	public class SpanWithinQuery : QueryBase, ISpanWithinQuery
	{
		public ISpanQuery Big { get; set; }
		public ISpanQuery Little { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanWithin = this;

		internal static bool IsConditionless(ISpanWithinQuery q)
		{
			var exclude = q.Little as IQuery;
			var include = q.Big as IQuery;

			return (exclude == null && include == null)
				|| (include == null && exclude.Conditionless)
				|| (exclude == null && include.Conditionless)
				|| (exclude != null && exclude.Conditionless && include != null && include.Conditionless);
		}
	}

	public class SpanWithinQueryDescriptor<T>
		: QueryDescriptorBase<SpanWithinQueryDescriptor<T>, ISpanWithinQuery>
			, ISpanWithinQuery where T : class
	{
		protected override bool Conditionless => SpanWithinQuery.IsConditionless(this);
		ISpanQuery ISpanWithinQuery.Big { get; set; }
		ISpanQuery ISpanWithinQuery.Little { get; set; }

		public SpanWithinQueryDescriptor<T> Big(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(a => a.Big = selector(new SpanQueryDescriptor<T>()));

		public SpanWithinQueryDescriptor<T> Little(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(a => a.Little = selector(new SpanQueryDescriptor<T>()));
	}
}
