using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SpanNotQuery>))]
	public interface ISpanNotQuery : ISpanSubQuery
	{
		[JsonProperty(PropertyName = "include")]
		ISpanQuery Include { get; set; }

		[JsonProperty(PropertyName = "exclude")]
		ISpanQuery Exclude { get; set; }

		[JsonProperty(PropertyName = "pre")]
		int? Pre { get; set; }

		[JsonProperty(PropertyName = "post")]
		int? Post { get; set; }

		[JsonProperty(PropertyName = "dist")]
		int? Dist { get; set; }

	}

	public class SpanNotQuery : QueryBase, ISpanNotQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public ISpanQuery Include { get; set; }
		public ISpanQuery Exclude { get; set; }
		public int? Pre { get; set; }
		public int? Post { get; set; }
		public int? Dist { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanNot = this;
		internal static bool IsConditionless(ISpanNotQuery q)
		{
			var exclude = q.Exclude as IQuery;
			var include = q.Include as IQuery;

			return (exclude == null && include == null)
				|| (include == null && exclude.Conditionless)
				|| (exclude == null && include.Conditionless)
				|| (exclude != null && exclude.Conditionless && include != null && include.Conditionless);
		}
	}

	public class SpanNotQueryDescriptor<T>
		: QueryDescriptorBase<SpanNotQueryDescriptor<T>, ISpanNotQuery>
		, ISpanNotQuery where T : class
	{
		protected override bool Conditionless => SpanNotQuery.IsConditionless(this);
		ISpanQuery ISpanNotQuery.Include { get; set; }
		ISpanQuery ISpanNotQuery.Exclude { get; set; }
		int? ISpanNotQuery.Pre { get; set; }
		int? ISpanNotQuery.Post { get; set; }
		int? ISpanNotQuery.Dist { get; set; }

		public SpanNotQueryDescriptor<T> Include(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(a => a.Include = selector(new SpanQueryDescriptor<T>()));

		public SpanNotQueryDescriptor<T> Exclude(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(a => a.Exclude = selector(new SpanQueryDescriptor<T>()));

		public SpanNotQueryDescriptor<T> Pre(int? pre) => Assign(a => a.Pre = pre);

		public SpanNotQueryDescriptor<T> Post(int? post) => Assign(a => a.Post = post);

		public SpanNotQueryDescriptor<T> Dist(int? dist) => Assign(a => a.Dist = dist);
	}
}
