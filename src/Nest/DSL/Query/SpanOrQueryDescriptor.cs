using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanOrQueryDescriptor<T> : ISpanQuery, IQuery where T : class
	{
		[JsonProperty(PropertyName = "clauses")]
		internal IEnumerable<SpanQueryDescriptor<T>> _SpanQueryDescriptors { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !_SpanQueryDescriptors.HasAny() || _SpanQueryDescriptors.Cast<IQuery>().All(q => q.IsConditionless);
			}
		}

		public SpanOrQueryDescriptor<T> Clauses(params Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>>[] selectors)
		{
			selectors.ThrowIfNull("selector");
			var descriptors = new List<SpanQueryDescriptor<T>>();
			foreach (var selector in selectors)
			{
				var span = new SpanQueryDescriptor<T>();
				var q = selector(span);
				if ((q as IQuery).IsConditionless)
					continue;

				descriptors.Add(q);
			}
			this._SpanQueryDescriptors = descriptors.HasAny() ? descriptors : null;
			return this;
		}
	}
}
