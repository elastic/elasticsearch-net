using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SpanNearQueryDescriptor<object>>))]
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

	public class SpanNearQuery : PlainQuery, ISpanNearQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanNear = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public IEnumerable<ISpanQuery> Clauses { get; set; }
		public int? Slop { get; set; }
		public bool? InOrder { get; set; }
		public bool? CollectPayloads { get; set; }
	}

	public class SpanNearQueryDescriptor<T> : ISpanNearQuery where T : class
	{
		IEnumerable<ISpanQuery> ISpanNearQuery.Clauses { get; set; }

		int? ISpanNearQuery.Slop { get; set; }

		bool? ISpanNearQuery.InOrder { get; set; }

		bool? ISpanNearQuery.CollectPayloads { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((ISpanNearQuery)this).Clauses.HasAny() 
					|| ((ISpanNearQuery)this).Clauses.Cast<IQuery>().All(q => q.IsConditionless);
			}
		}

		public SpanNearQueryDescriptor<T> Clauses(params Func<SpanQuery<T>, SpanQuery<T>>[] selectors)
		{
			selectors.ThrowIfNull("selector");
			var descriptors = new List<SpanQuery<T>>();
			foreach (var selector in selectors)
			{
				var x = new SpanQuery<T>();
				var q = selector(x);
				if ((q as IQuery).IsConditionless)
					continue;

				descriptors.Add(q);

			}
			((ISpanNearQuery)this).Clauses = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		public SpanNearQueryDescriptor<T> Slop(int slop)
		{
			((ISpanNearQuery)this).Slop = slop;
			return this;
		}
		public SpanNearQueryDescriptor<T> InOrder(bool inOrder)
		{
			((ISpanNearQuery)this).InOrder = inOrder;
			return this;
		}
		public SpanNearQueryDescriptor<T> CollectPayloads(bool collectPayloads)
		{
			((ISpanNearQuery)this).CollectPayloads = collectPayloads;
			return this;
		}
	}
}
