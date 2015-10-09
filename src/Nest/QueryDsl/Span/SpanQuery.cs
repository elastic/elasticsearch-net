using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SpanQueryDescriptor<object>>))]
	public interface ISpanQuery : IQuery
	{
		[JsonProperty(PropertyName = "span_term")]
		ISpanTermQuery SpanTermQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_first")]
		ISpanFirstQuery SpanFirst { get; set; }

		[JsonProperty(PropertyName = "span_near")]
		ISpanNearQuery SpanNear { get; set; }

		[JsonProperty(PropertyName = "span_or")]
		ISpanOrQuery SpanOr { get; set; }

		[JsonProperty(PropertyName = "span_not")]
		ISpanNotQuery SpanNot { get; set; }

		[JsonProperty(PropertyName = "span_multi")]
		ISpanMultiTermQuery SpanMultiTerm { get; set; }
	}

	public class SpanQuery : ISpanQuery
	{
		string IQuery.Name { get; set; }
		double? IQuery.Boost { get; set; }
		bool IQuery.Conditionless => IsConditionless(this);
		public ISpanTermQuery SpanTermQueryDescriptor { get; set; }
		public ISpanFirstQuery SpanFirst { get; set; }
		public ISpanNearQuery SpanNear { get; set; }
		public ISpanOrQuery SpanOr { get; set; }
		public ISpanNotQuery SpanNot { get; set; }
		public ISpanMultiTermQuery SpanMultiTerm { get; set; }

		internal static bool IsConditionless(ISpanQuery q)
		{
			var queries = new[]
			{
				q.SpanTermQueryDescriptor as IQuery,
				q.SpanFirst as IQuery,
				q.SpanNear as IQuery,
				q.SpanOr as IQuery,
				q.SpanNot as IQuery,
				q.SpanMultiTerm as IQuery
			};

			return queries.All(sq => sq == null || sq.Conditionless);
		}
	}

	public class SpanQueryDescriptor<T> 
		: QueryDescriptorBase<SpanQueryDescriptor<T>, ISpanQuery>
		, ISpanQuery where T : class
	{
		bool IQuery.Conditionless => SpanQuery.IsConditionless(this);
		ISpanTermQuery ISpanQuery.SpanTermQueryDescriptor { get; set; }
		ISpanFirstQuery ISpanQuery.SpanFirst { get; set; }
		ISpanNearQuery ISpanQuery.SpanNear { get; set; }
		ISpanOrQuery ISpanQuery.SpanOr { get; set; }
		ISpanNotQuery ISpanQuery.SpanNot { get; set; }
		ISpanMultiTermQuery ISpanQuery.SpanMultiTerm { get; set; }

		public SpanQueryDescriptor<T> SpanTerm(Expression<Func<T, object>> fieldDescriptor
			, string value	
			, double? Boost = null)
		{
			if (fieldDescriptor == null || value.IsNullOrEmpty())
				return this;

			var spanTerm = new SpanTermQueryDescriptor<T>();
			((ITermQuery)spanTerm).Field = fieldDescriptor;
			((ITermQuery)spanTerm).Value = value;
			((ITermQuery)spanTerm).Boost = Boost;
			return CreateQuery(spanTerm, (sq) => sq.SpanTermQueryDescriptor = spanTerm);
		}
		
		public SpanQueryDescriptor<T> SpanTerm(string field, string value, double? Boost = null)
		{
			if (field.IsNullOrEmpty() || value.IsNullOrEmpty())
				return this;

			var spanTerm = new SpanTermQueryDescriptor<T>();
			((ITermQuery)spanTerm).Field = field;
			((ITermQuery)spanTerm).Value = value;
			((ITermQuery)spanTerm).Boost = Boost;
			return CreateQuery(spanTerm, (sq) => sq.SpanTermQueryDescriptor = spanTerm);
		}
		
		public SpanQueryDescriptor<T> SpanFirst(Func<SpanFirstQueryDescriptor<T>, SpanFirstQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanFirstQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanFirst = q);
		}
		
		public SpanQueryDescriptor<T> SpanNear(Func<SpanNearQueryDescriptor<T>, SpanNearQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanNearQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanNear = q);
		}

		public SpanQueryDescriptor<T> SpanOr(Func<SpanOrQueryDescriptor<T>, SpanOrQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanOrQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanOr = q);
		}

		public SpanQueryDescriptor<T> SpanNot(Func<SpanNotQueryDescriptor<T>, SpanNotQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q = selector(new SpanNotQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanNot = q);
		}

		public SpanQueryDescriptor<T> SpanMultiTerm(Func<SpanMultiTermQueryDescriptor<T>, SpanMultiTermQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var q= selector(new SpanMultiTermQueryDescriptor<T>());
			return CreateQuery(q, (sq) => sq.SpanMultiTerm = q);
		}

		private SpanQueryDescriptor<T> CreateQuery<K>(K query, Action<ISpanQuery> setProperty) where K : ISpanSubQuery
		{
			if (((IQuery)(query)).Conditionless)
				return this;

			var newSpanQuery = new SpanQueryDescriptor<T>();
			setProperty(newSpanQuery);
			return newSpanQuery;
		}
	}
}
