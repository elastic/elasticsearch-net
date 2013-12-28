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
		private bool _spanNewQuery = false;
		public SpanQueryDescriptor() : this(false)
		{
		}

		internal SpanQueryDescriptor(bool forceNewQuery)
		{
			this._spanNewQuery = forceNewQuery;
		}
		
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

		bool IQuery.IsConditionless
		{
			get
			{
				return !this._spanNewQuery;
			}
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
				return this;

			var spanTerm = new SpanTerm() { Field = field, Value = value, Boost = Boost};
			return CreateQuery(spanTerm, (sq) => sq.SpanTermQuery = spanTerm);

		}
		
		public SpanQueryDescriptor<T> SpanFirst(Func<SpanFirstQueryDescriptor<T>, SpanFirstQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanFirstQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanFirstQueryDescriptor = q);
		}
		
		public SpanQueryDescriptor<T> SpanNear(Func<SpanNearQueryDescriptor<T>, SpanNearQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanNearQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanNearQueryDescriptor = q);
		}
		public SpanQueryDescriptor<T> SpanOr(Func<SpanOrQueryDescriptor<T>, SpanOrQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanOrQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanOrQueryDescriptor = q);
		}
		public SpanQueryDescriptor<T> SpanNot(Func<SpanNotQueryDescriptor<T>, SpanNotQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanNotQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanNotQueryDescriptor = q);
		}

		private SpanQueryDescriptor<T> CreateQuery<K>(K query, Action<SpanQueryDescriptor<T>> setProperty) where K : ISpanQuery
		{
			if (((IQuery)(query)).IsConditionless)
				return this;

			var newSpanQuery = new SpanQueryDescriptor<T>(true);
			setProperty(newSpanQuery);
			_spanNewQuery = true;
			return newSpanQuery;
		}
	}
}
