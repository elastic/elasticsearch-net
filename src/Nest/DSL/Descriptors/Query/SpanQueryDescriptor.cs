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
	public class SpanQueryDescriptor<T> : IQuery where T : class
	{

		[JsonProperty(PropertyName = "span_term")]
		internal SpanTerm SpanTermQuery { get; set; }

		[JsonProperty(PropertyName = "span_first")]
		internal SpanFirstQueryDescriptor<T> SpanFirstQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_near")]
		internal SpanNearQueryDescriptor<T> SpanNearQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_or")]
		internal SpanOrQueryDescriptor<T> SpanOrQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_not")]
		internal SpanNotQueryDescriptor<T> SpanNotQueryDescriptor { get; set; }

		public bool IsConditionless { get; set; }

		internal static SpanQueryDescriptor<T> CreateConditionlessSpanQueryDescriptor()
		{
			return new SpanQueryDescriptor<T> { IsConditionless = true };
		}

		public SpanQueryDescriptor<T> SpanTerm(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.SpanTerm(field, value, Boost: Boost);
		}
		public SpanQueryDescriptor<T> SpanTerm(string field, string value, double? Boost = null)
		{
			if (field.IsNullOrEmpty() || value.IsNullOrEmpty())
				return CreateConditionlessSpanQueryDescriptor();

			var spanTerm = new SpanTerm() { Field = field, Value = value };
			if (Boost.HasValue)
				spanTerm.Boost = Boost;
			this.SpanTermQuery = spanTerm;
			return new SpanQueryDescriptor<T> { SpanTermQuery = this.SpanTermQuery };
		}
		public SpanQueryDescriptor<T> SpanFirst(Func<SpanFirstQueryDescriptor<T>, SpanFirstQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanFirstQueryDescriptor = selector(new SpanFirstQueryDescriptor<T>());
			if (this.SpanFirstQueryDescriptor.IsConditionless)
				return CreateConditionlessSpanQueryDescriptor();

			return new SpanQueryDescriptor<T> { SpanFirstQueryDescriptor = this.SpanFirstQueryDescriptor };
		}
		public SpanQueryDescriptor<T> SpanNear(Func<SpanNearQueryDescriptor<T>, SpanNearQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanNearQueryDescriptor = selector(new SpanNearQueryDescriptor<T>());
			return new SpanQueryDescriptor<T> { SpanNearQueryDescriptor = this.SpanNearQueryDescriptor };
		}
		public SpanQueryDescriptor<T> SpanOr(Func<SpanOrQueryDescriptor<T>, SpanOrQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanOrQueryDescriptor = selector(new SpanOrQueryDescriptor<T>());
			return new SpanQueryDescriptor<T> { SpanOrQueryDescriptor = this.SpanOrQueryDescriptor };
		}
		public SpanQueryDescriptor<T> SpanNot(Func<SpanNotQueryDescriptor<T>, SpanNotQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanNotQueryDescriptor = selector(new SpanNotQueryDescriptor<T>());
			return new SpanQueryDescriptor<T> { SpanNotQueryDescriptor = this.SpanNotQueryDescriptor };
		}
	}
}
