using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	public interface ISpanSubQuery {}

	public interface ISpanQuery 
	{
		[JsonProperty(PropertyName = "span_term")]
		SpanTerm SpanTermQuery { get; set; }

		[JsonProperty(PropertyName = "span_first")]
		ISpanFirstQuery SpanFirstQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_near")]
		ISpanNearQuery SpanNearQuery { get; set; }

		[JsonProperty(PropertyName = "span_or")]
		ISpanOrQuery SpanOrQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_not")]
		ISpanNotQuery SpanNotQuery { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanQuery<T> : IQuery, ISpanQuery where T : class
	{
		
		[JsonProperty(PropertyName = "span_term")]
		SpanTerm ISpanQuery.SpanTermQuery { get; set; }

		[JsonProperty(PropertyName = "span_first")]
		ISpanFirstQuery ISpanQuery.SpanFirstQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_near")]
		ISpanNearQuery ISpanQuery.SpanNearQuery { get; set; }

		[JsonProperty(PropertyName = "span_or")]
		ISpanOrQuery ISpanQuery.SpanOrQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_not")]
		ISpanNotQuery ISpanQuery.SpanNotQuery { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				var queries = new[]
				{
					((ISpanQuery)this).SpanTermQuery as IQuery,
					((ISpanQuery)this).SpanFirstQueryDescriptor as IQuery,
					((ISpanQuery)this).SpanNearQuery as IQuery,
					((ISpanQuery)this).SpanOrQueryDescriptor as IQuery,
					((ISpanQuery)this).SpanNotQuery as IQuery
				};
				return queries.All(q => q == null || q.IsConditionless);
			}
		}

		public SpanQuery<T> SpanTerm(Expression<Func<T, object>> fieldDescriptor
			, string value	
			, double? Boost = null)
		{
			if (fieldDescriptor == null || value.IsNullOrEmpty())
				return this;

			var spanTerm = new SpanTerm() { Field = fieldDescriptor, Value = value, Boost = Boost};
			return CreateQuery(spanTerm, (sq) => ((ISpanQuery)sq).SpanTermQuery = spanTerm);
		}
		
		public SpanQuery<T> SpanTerm(string field, string value, double? Boost = null)
		{
			if (field.IsNullOrEmpty() || value.IsNullOrEmpty())
				return this;

			var spanTerm = new SpanTerm() { Field = field, Value = value, Boost = Boost};
			return CreateQuery(spanTerm, (sq) => ((ISpanQuery)sq).SpanTermQuery = spanTerm);

		}
		
		public SpanQuery<T> SpanFirst(Func<SpanFirstQueryDescriptor<T>, SpanFirstQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanFirstQueryDescriptor<T>());
			return CreateQuery(q, (sq) => ((ISpanQuery)sq).SpanFirstQueryDescriptor = q);
		}
		
		public SpanQuery<T> SpanNear(Func<SpanNearQuery<T>, SpanNearQuery<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanNearQuery<T>());
			return CreateQuery(q, (sq) => ((ISpanQuery)sq).SpanNearQuery = q);
		}
		public SpanQuery<T> SpanOr(Func<SpanOrQueryDescriptor<T>, SpanOrQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanOrQueryDescriptor<T>());
			return CreateQuery(q, (sq) => ((ISpanQuery)sq).SpanOrQueryDescriptor = q);
		}
		public SpanQuery<T> SpanNot(Func<SpanNotQuery<T>, SpanNotQuery<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanNotQuery<T>());
			return CreateQuery(q, (sq) => ((ISpanQuery)sq).SpanNotQuery = q);
		}

		private SpanQuery<T> CreateQuery<K>(K query, Action<SpanQuery<T>> setProperty) where K : ISpanSubQuery
		{
			if (((IQuery)(query)).IsConditionless)
				return this;

			var newSpanQuery = new SpanQuery<T>();
			setProperty(newSpanQuery);
			return newSpanQuery;
		}
	}
}
