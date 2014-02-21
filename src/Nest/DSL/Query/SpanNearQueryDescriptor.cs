using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanNearQueryDescriptor<T> : ISpanQuery, IQuery where T : class
	{
		[JsonProperty(PropertyName = "clauses")]
		internal IEnumerable<SpanQueryDescriptor<T>> _SpanQueryDescriptors { get; set; }

		[JsonProperty(PropertyName = "slop")]
		internal int? _Slop { get; set; }

		[JsonProperty(PropertyName = "in_order")]
		internal bool? _InOrder { get; set; }

		[JsonProperty(PropertyName = "collect_payloads")]
		internal bool? _CollectPayloads { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !_SpanQueryDescriptors.HasAny() || _SpanQueryDescriptors.Cast<IQuery>().All(q => q.IsConditionless);
			}
		}

		public SpanNearQueryDescriptor<T> Clauses(params Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>>[] selectors)
		{
			selectors.ThrowIfNull("selector");
			var descriptors = new List<SpanQueryDescriptor<T>>();
			foreach (var selector in selectors)
			{
				var x = new SpanQueryDescriptor<T>();
				var q = selector(x);
				if ((q as IQuery).IsConditionless)
					continue;

				descriptors.Add(q);

			}
			this._SpanQueryDescriptors = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		public SpanNearQueryDescriptor<T> Slop(int slop)
		{
			this._Slop = slop;
			return this;
		}
		public SpanNearQueryDescriptor<T> InOrder(bool inOrder)
		{
			this._InOrder = inOrder;
			return this;
		}
		public SpanNearQueryDescriptor<T> CollectPayloads(bool collectPayloads)
		{
			this._CollectPayloads = collectPayloads;
			return this;
		}
	}
}
