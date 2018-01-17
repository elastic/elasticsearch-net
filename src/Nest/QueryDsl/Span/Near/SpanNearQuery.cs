using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SpanNearQueryDescriptor<object>>))]
	public interface ISpanNearQuery : ISpanSubQuery
	{
		[JsonProperty("clauses")]
		IEnumerable<ISpanQuery> Clauses { get; set; }

		[JsonProperty("slop")]
		int? Slop { get; set; }

		[JsonProperty("in_order")]
		bool? InOrder { get; set; }
	}

	public class SpanNearQuery : QueryBase, ISpanNearQuery
	{
		protected override bool Conditionless => SpanNearQuery.IsConditionless(this);
		public IEnumerable<ISpanQuery> Clauses { get; set; }
		public int? Slop { get; set; }
		public bool? InOrder { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanNear = this;
		internal static bool IsConditionless(ISpanNearQuery q) => !q.Clauses.HasAny() || q.Clauses.Cast<IQuery>().All(qq => qq.Conditionless);
	}

	public class SpanNearQueryDescriptor<T>
		: QueryDescriptorBase<SpanNearQueryDescriptor<T>, ISpanNearQuery>
		, ISpanNearQuery where T : class
	{
		protected override bool Conditionless => SpanNearQuery.IsConditionless(this);
		IEnumerable<ISpanQuery> ISpanNearQuery.Clauses { get; set; }
		int? ISpanNearQuery.Slop { get; set; }
		bool? ISpanNearQuery.InOrder { get; set; }

		public SpanNearQueryDescriptor<T> Clauses(params Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>>[] selectors) => Clauses(selectors.ToList());

		public SpanNearQueryDescriptor<T> Clauses(IEnumerable<Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>>> selectors) => Assign(a =>
		{
			a.Clauses = selectors.Select(selector => selector?.Invoke(new SpanQueryDescriptor<T>()))
				.Where(query => query != null && !((IQuery) query).Conditionless).ToListOrNullIfEmpty();
		});

		public SpanNearQueryDescriptor<T> Slop(int? slop) => Assign(a => a.Slop = slop);

		public SpanNearQueryDescriptor<T> InOrder(bool? inOrder = true) => Assign(a => a.InOrder = inOrder);

	}
}
