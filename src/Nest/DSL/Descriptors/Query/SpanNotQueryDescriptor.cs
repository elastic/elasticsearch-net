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

		internal bool IsConditionless
		{
			get
			{
				return this._Include == null && this._Exclude == null
					|| (this._Include != null && this._Include.IsConditionless)
					|| (this._Exclude != null && this._Exclude.IsConditionless);
			}
		}

		public SpanNotQueryDescriptor<T> Include(Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>> selector)
		{
			if (selector == null)
				return this;
			var descriptors = new List<SpanQueryDescriptor<T>>();
			var span = new SpanQueryDescriptor<T>();
			var q = selector(span);
			this._Include = q;
			return this;
		}
		public SpanNotQueryDescriptor<T> Exclude(Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>> selector)
		{
			if (selector == null)
				return this;
			var descriptors = new List<SpanQueryDescriptor<T>>();
			var span = new SpanQueryDescriptor<T>();
			var q = selector(span);
			this._Exclude = q;
			return this;
		}
	}
}
