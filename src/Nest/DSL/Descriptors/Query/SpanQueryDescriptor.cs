using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public class SpanQueryDescriptor<T> where T : class
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


		public void SpanTerm(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.SpanTerm(field, value, Boost: Boost);
		}
		public void SpanTerm(string field, string value, double? Boost = null)
		{
			var spanTerm = new SpanTerm() { Field = field, Value = value };
			if (Boost.HasValue)
				spanTerm.Boost = Boost;
			this.SpanTermQuery = spanTerm;
		}
		public void SpanFirst(Func<SpanFirstQueryDescriptor<T>, SpanFirstQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanFirstQueryDescriptor = selector(new SpanFirstQueryDescriptor<T>());
		}
		public void SpanNear(Func<SpanNearQueryDescriptor<T>, SpanNearQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanNearQueryDescriptor = selector(new SpanNearQueryDescriptor<T>());
		}
		public void SpanOr(Func<SpanOrQueryDescriptor<T>, SpanOrQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanOrQueryDescriptor = selector(new SpanOrQueryDescriptor<T>());
		}
		public void SpanNot(Func<SpanNotQueryDescriptor<T>, SpanNotQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanNotQueryDescriptor = selector(new SpanNotQueryDescriptor<T>());
		}
	}
}
