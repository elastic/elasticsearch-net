using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public class SpanNotQueryDescriptor<T>  : ISpanQuery where T : class
	{
		[JsonProperty(PropertyName = "include")]
		internal SpanQueryDescriptor<T> _Include { get; set; }
		[JsonProperty(PropertyName = "exclude")]
		internal SpanQueryDescriptor<T> _Exclude { get; set; }

		public SpanNotQueryDescriptor<T> Include(Action<SpanQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var descriptors = new List<SpanQueryDescriptor<T>>();
			var span = new SpanQueryDescriptor<T>();
			selector(span);
			this._Include = span;
			return this;
		}
		public SpanNotQueryDescriptor<T> Exclude(Action<SpanQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var descriptors = new List<SpanQueryDescriptor<T>>();
			var span = new SpanQueryDescriptor<T>();
			selector(span);
			this._Exclude = span;
			return this;
		}
	}
}
