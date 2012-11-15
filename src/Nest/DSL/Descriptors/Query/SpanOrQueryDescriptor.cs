using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public class SpanOrQueryDescriptor<T>  : ISpanQuery where T : class
	{
		[JsonProperty(PropertyName = "clauses")]
		internal IEnumerable<SpanQueryDescriptor<T>> _SpanQueryDescriptors { get; set; }

		internal bool IsConditionless
		{
			get
			{
				return !_SpanQueryDescriptors.HasAny() || _SpanQueryDescriptors.All(q => q.IsConditionless);
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
				if (q.IsConditionless)
					continue;

				descriptors.Add(q);
			}
			this._SpanQueryDescriptors = descriptors;
			return this;
		}
	}
}
