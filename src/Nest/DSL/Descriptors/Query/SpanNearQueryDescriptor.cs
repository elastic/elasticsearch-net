using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest.DSL
{
	public class SpanNearQueryDescriptor<T> : ISpanQuery where T : class
	{
		[JsonProperty(PropertyName = "clauses")]
		internal IEnumerable<SpanQueryDescriptor<T>> _SpanQueryDescriptors { get; set; }

		[JsonProperty(PropertyName = "slop")]
		internal int? _Slop { get; set; }

		[JsonProperty(PropertyName = "in_order")]
		internal bool? _InOrder { get; set; }

		[JsonProperty(PropertyName = "collect_payloads")]
		internal bool? _CollectPayloads { get; set; }

		public SpanNearQueryDescriptor<T> Clauses(params Action<SpanQueryDescriptor<T>>[] selectors)
		{
			selectors.ThrowIfNull("selector");
			var descriptors = new List<SpanQueryDescriptor<T>>();
			foreach (var selector in selectors)
			{
				var x = new SpanQueryDescriptor<T>();
				selector(x);
				descriptors.Add(x);
			}
			this._SpanQueryDescriptors = descriptors;
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
