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
		ISpanTermQuery SpanTerm { get; set; }

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
		public ISpanTermQuery SpanTerm { get; set; }
		public ISpanFirstQuery SpanFirst { get; set; }
		public ISpanNearQuery SpanNear { get; set; }
		public ISpanOrQuery SpanOr { get; set; }
		public ISpanNotQuery SpanNot { get; set; }
		public ISpanMultiTermQuery SpanMultiTerm { get; set; }

		internal static bool IsConditionless(ISpanQuery q) => new[]
		{
			q.SpanTerm as IQuery,
			q.SpanFirst,
			q.SpanNear,
			q.SpanOr ,
			q.SpanNot,
			q.SpanMultiTerm
		}.All(sq => sq == null || sq.Conditionless);
	}

	public class SpanQueryDescriptor<T> : QueryDescriptorBase<SpanQueryDescriptor<T>, ISpanQuery>
		, ISpanQuery where T : class
	{
		bool IQuery.Conditionless => SpanQuery.IsConditionless(this);
		ISpanTermQuery ISpanQuery.SpanTerm { get; set; }
		ISpanFirstQuery ISpanQuery.SpanFirst { get; set; }
		ISpanNearQuery ISpanQuery.SpanNear { get; set; }
		ISpanOrQuery ISpanQuery.SpanOr { get; set; }
		ISpanNotQuery ISpanQuery.SpanNot { get; set; }
		ISpanMultiTermQuery ISpanQuery.SpanMultiTerm { get; set; }

		public SpanQueryDescriptor<T> SpanTerm(Func<SpanTermQueryDescriptor<T>, ISpanTermQuery> selector) =>
			Assign(a => a.SpanTerm = selector?.Invoke(new SpanTermQueryDescriptor<T>()));
		
		public SpanQueryDescriptor<T> SpanFirst(Func<SpanFirstQueryDescriptor<T>, SpanFirstQueryDescriptor<T>> selector) =>
			Assign(a => a.SpanFirst = selector?.Invoke(new SpanFirstQueryDescriptor<T>()));
		
		public SpanQueryDescriptor<T> SpanNear(Func<SpanNearQueryDescriptor<T>, SpanNearQueryDescriptor<T>> selector) =>
			Assign(a => a.SpanNear = selector?.Invoke(new SpanNearQueryDescriptor<T>()));
		

		public SpanQueryDescriptor<T> SpanOr(Func<SpanOrQueryDescriptor<T>, SpanOrQueryDescriptor<T>> selector) =>
			Assign(a => a.SpanOr = selector?.Invoke(new SpanOrQueryDescriptor<T>()));
		

		public SpanQueryDescriptor<T> SpanNot(Func<SpanNotQueryDescriptor<T>, SpanNotQueryDescriptor<T>> selector) =>
			Assign(a => a.SpanNot = selector?.Invoke(new SpanNotQueryDescriptor<T>()));
		

		public SpanQueryDescriptor<T> SpanMultiTerm(Func<SpanMultiTermQueryDescriptor<T>, SpanMultiTermQueryDescriptor<T>> selector) =>
			Assign(a => a.SpanMultiTerm = selector?.Invoke(new SpanMultiTermQueryDescriptor<T>()));
		
	}
}
