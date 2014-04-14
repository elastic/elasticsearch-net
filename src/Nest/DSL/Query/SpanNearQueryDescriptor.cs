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
	public interface ISpanNearQuery 
	{
		[JsonProperty(PropertyName = "clauses")]
		IEnumerable<ISpanQuery> _SpanQueryDescriptors { get; set; }

		[JsonProperty(PropertyName = "slop")]
		int? _Slop { get; set; }

		[JsonProperty(PropertyName = "in_order")]
		bool? _InOrder { get; set; }

		[JsonProperty(PropertyName = "collect_payloads")]
		bool? _CollectPayloads { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanNearQuery<T> : ISpanSubQuery, IQuery, ISpanNearQuery where T : class
	{
		IEnumerable<ISpanQuery> ISpanNearQuery._SpanQueryDescriptors { get; set; }

		int? ISpanNearQuery._Slop { get; set; }

		bool? ISpanNearQuery._InOrder { get; set; }

		bool? ISpanNearQuery._CollectPayloads { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((ISpanNearQuery)this)._SpanQueryDescriptors.HasAny() 
					|| ((ISpanNearQuery)this)._SpanQueryDescriptors.Cast<IQuery>().All(q => q.IsConditionless);
			}
		}

		public SpanNearQuery<T> Clauses(params Func<SpanQuery<T>, SpanQuery<T>>[] selectors)
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
			((ISpanNearQuery)this)._SpanQueryDescriptors = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		public SpanNearQuery<T> Slop(int slop)
		{
			((ISpanNearQuery)this)._Slop = slop;
			return this;
		}
		public SpanNearQuery<T> InOrder(bool inOrder)
		{
			((ISpanNearQuery)this)._InOrder = inOrder;
			return this;
		}
		public SpanNearQuery<T> CollectPayloads(bool collectPayloads)
		{
			((ISpanNearQuery)this)._CollectPayloads = collectPayloads;
			return this;
		}
	}
}
