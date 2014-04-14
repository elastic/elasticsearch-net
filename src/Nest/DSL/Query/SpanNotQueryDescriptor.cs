using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISpanNotQuery
	{
		[JsonProperty(PropertyName = "include")]
		ISpanQuery _Include { get; set; }

		[JsonProperty(PropertyName = "exclude")]
		ISpanQuery _Exclude { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanNotQuery<T> : ISpanSubQuery, IQuery, ISpanNotQuery where T : class
	{
		ISpanQuery ISpanNotQuery._Include { get; set; }

		ISpanQuery ISpanNotQuery._Exclude { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				var excludeQuery = ((ISpanNotQuery)this)._Exclude as IQuery;
				var includeQuery = ((ISpanNotQuery)this)._Include as IQuery;

				return excludeQuery == null && includeQuery == null
					|| (includeQuery == null && excludeQuery.IsConditionless)
					|| (excludeQuery == null && includeQuery.IsConditionless)
					|| (excludeQuery != null && excludeQuery.IsConditionless && includeQuery != null && includeQuery.IsConditionless);
			}
		}



		public SpanNotQuery<T> Include(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			if (selector == null)
				return this;
			var descriptors = new List<SpanQuery<T>>();
			var span = new SpanQuery<T>();
			var q = selector(span);
			((ISpanNotQuery)this)._Include = q;
			return this;
		}
		public SpanNotQuery<T> Exclude(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			if (selector == null)
				return this;
			var descriptors = new List<SpanQuery<T>>();
			var span = new SpanQuery<T>();
			var q = selector(span);
			((ISpanNotQuery)this)._Exclude = q;
			return this;
		}
	}
}
