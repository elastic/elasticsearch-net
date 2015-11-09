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
		[JsonProperty(PropertyName = "clauses")]
		IEnumerable<ISpanQuery> Clauses { get; set; }

		[JsonProperty(PropertyName = "slop")]
		int? Slop { get; set; }

		[JsonProperty(PropertyName = "in_order")]
		bool? InOrder { get; set; }

		[JsonProperty(PropertyName = "collect_payloads")]
		bool? CollectPayloads { get; set; }
	}

	public class SpanNearQuery : QueryBase, ISpanNearQuery
	{
		bool IQuery.Conditionless { get { return false; } }
		public IEnumerable<ISpanQuery> Clauses { get; set; }
		public int? Slop { get; set; }
		public bool? InOrder { get; set; }
		public bool? CollectPayloads { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.SpanNear = this;
		internal static bool IsConditionless(ISpanNearQuery q) => !q.Clauses.HasAny() || q.Clauses.Cast<IQuery>().All(qq => qq.Conditionless);
	}

	public class SpanNearQueryDescriptor<T> 
		: QueryDescriptorBase<SpanNearQueryDescriptor<T>, ISpanNearQuery>
		, ISpanNearQuery where T : class
	{
		bool IQuery.Conditionless => SpanNearQuery.IsConditionless(this);
		IEnumerable<ISpanQuery> ISpanNearQuery.Clauses { get; set; }
		int? ISpanNearQuery.Slop { get; set; }
		bool? ISpanNearQuery.InOrder { get; set; }
		bool? ISpanNearQuery.CollectPayloads { get; set; }

		public SpanNearQueryDescriptor<T> Clauses(params Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>>[] selectors) => Assign(a =>
		{
			var clauses = new List<SpanQueryDescriptor<T>>();
			foreach (var selector in selectors)
			{
				var query = selector(new SpanQueryDescriptor<T>());
				if (((IQuery)query).Conditionless) continue;
				clauses.Add(query);
			}
			a.Clauses = clauses.HasAny() ? clauses : null;
		});

		public SpanNearQueryDescriptor<T> Slop(int slop) => Assign(a => a.Slop = slop);

		public SpanNearQueryDescriptor<T> InOrder(bool inOrder) => Assign(a => a.InOrder = inOrder);

		public SpanNearQueryDescriptor<T> CollectPayloads(bool collectPayloads) => Assign(a => a.CollectPayloads = collectPayloads);
	}
}
