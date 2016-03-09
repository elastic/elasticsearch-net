using System;
using System.Linq;
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

		[JsonProperty(PropertyName = "span_containing")]
		ISpanContainingQuery SpanContaining { get; set; }

		[JsonProperty(PropertyName = "span_within")]
		ISpanWithinQuery SpanWithin { get; set; }

		[JsonProperty(PropertyName = "span_multi")]
		ISpanMultiTermQuery SpanMultiTerm { get; set; }

		void Accept(IQueryVisitor visitor);
	}

	public class SpanQuery : ISpanQuery
	{
		string IQuery.Name { get; set; }
		double? IQuery.Boost { get; set; }
		bool IQuery.IsWritable { get; }
		public bool IsVerbatim { get; set; }
		public bool IsStrict { get; set; }
		public bool IsWritable => this.IsVerbatim || !IsConditionless(this);
		bool IQuery.Conditionless => IsConditionless(this);
		public ISpanTermQuery SpanTerm { get; set; }
		public ISpanFirstQuery SpanFirst { get; set; }
		public ISpanNearQuery SpanNear { get; set; }
		public ISpanOrQuery SpanOr { get; set; }
		public ISpanNotQuery SpanNot { get; set; }
		public ISpanMultiTermQuery SpanMultiTerm { get; set; }
		public ISpanContainingQuery SpanContaining{ get; set; }
		public ISpanWithinQuery SpanWithin { get; set; }
		public void Accept(IQueryVisitor visitor) => new QueryWalker().Walk(this, visitor);

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
		protected override bool Conditionless => SpanQuery.IsConditionless(this);
		ISpanTermQuery ISpanQuery.SpanTerm { get; set; }
		ISpanFirstQuery ISpanQuery.SpanFirst { get; set; }
		ISpanNearQuery ISpanQuery.SpanNear { get; set; }
		ISpanOrQuery ISpanQuery.SpanOr { get; set; }
		ISpanNotQuery ISpanQuery.SpanNot { get; set; }
		ISpanMultiTermQuery ISpanQuery.SpanMultiTerm { get; set; }
		ISpanContainingQuery ISpanQuery.SpanContaining{ get; set; }
		ISpanWithinQuery ISpanQuery.SpanWithin { get; set; }

		public SpanQueryDescriptor<T> SpanTerm(Func<SpanTermQueryDescriptor<T>, ISpanTermQuery> selector) =>
			Assign(a => a.SpanTerm = selector?.Invoke(new SpanTermQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanFirst(Func<SpanFirstQueryDescriptor<T>, ISpanFirstQuery> selector) =>
			Assign(a => a.SpanFirst = selector?.Invoke(new SpanFirstQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanNear(Func<SpanNearQueryDescriptor<T>, ISpanNearQuery> selector) =>
			Assign(a => a.SpanNear = selector?.Invoke(new SpanNearQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanOr(Func<SpanOrQueryDescriptor<T>, ISpanOrQuery> selector) =>
			Assign(a => a.SpanOr = selector?.Invoke(new SpanOrQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanNot(Func<SpanNotQueryDescriptor<T>, ISpanNotQuery> selector) =>
			Assign(a => a.SpanNot = selector?.Invoke(new SpanNotQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanMultiTerm(Func<SpanMultiTermQueryDescriptor<T>, ISpanMultiTermQuery> selector) =>
			Assign(a => a.SpanMultiTerm = selector?.Invoke(new SpanMultiTermQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanContaining(Func<SpanContainingQueryDescriptor<T>, ISpanContainingQuery> selector) =>
			Assign(a => a.SpanContaining = selector?.Invoke(new SpanContainingQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanWithin(Func<SpanWithinQueryDescriptor<T>, ISpanWithinQuery> selector) =>
			Assign(a => a.SpanWithin = selector?.Invoke(new SpanWithinQueryDescriptor<T>()));

		void ISpanQuery.Accept(IQueryVisitor visitor) => new QueryWalker().Walk(this, visitor);

	}
}
