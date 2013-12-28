using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanFirstQueryDescriptor<T> : ISpanQuery, IQuery where T : class
	{
		[JsonProperty(PropertyName = "match")]
		internal SpanQueryDescriptor<T> _SpanQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "end")]
		internal int? _End { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return this._SpanQueryDescriptor == null || (_SpanQueryDescriptor as IQuery).IsConditionless;
			}
		}

		public SpanFirstQueryDescriptor<T> MatchTerm(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			this.MatchTerm(field, value, Boost: Boost);
			return this;
		}
		public SpanFirstQueryDescriptor<T> MatchTerm(string field, string value, double? Boost = null)
		{
			var span = new SpanQueryDescriptor<T>(true);
			span = span.SpanTerm(field, value, Boost);
			this._SpanQueryDescriptor = span;
			return this;
		}
		public SpanFirstQueryDescriptor<T> Match(Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this._SpanQueryDescriptor = selector(new SpanQueryDescriptor<T>());
			return this;
		}
		public SpanFirstQueryDescriptor<T> End(int end)
		{
			this._End = end;
			return this;
		}

	}
}
