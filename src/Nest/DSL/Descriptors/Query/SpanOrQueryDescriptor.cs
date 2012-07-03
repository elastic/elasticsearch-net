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

		public SpanOrQueryDescriptor<T> Clauses(params Action<SpanQueryDescriptor<T>>[] selectors)
		{
			selectors.ThrowIfNull("selector");
			var descriptors = new List<SpanQueryDescriptor<T>>();
			foreach (var selector in selectors)
			{
				var span = new SpanQueryDescriptor<T>();
				selector(span);
				descriptors.Add(span);
			}
			this._SpanQueryDescriptors = descriptors;
			return this;
		}
	}
}
