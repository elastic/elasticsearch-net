using System;
using System.Linq;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SpanQueryDescriptor<object>))]
	public interface ISpanQuery : IQuery
	{
		[DataMember(Name ="span_containing")]
		ISpanContainingQuery SpanContaining { get; set; }

		[DataMember(Name ="field_masking_span")]
		ISpanFieldMaskingQuery SpanFieldMasking { get; set; }

		[DataMember(Name ="span_first")]
		ISpanFirstQuery SpanFirst { get; set; }

		[DataMember(Name ="span_gap")]
		ISpanGapQuery SpanGap { get; set; }

		[DataMember(Name ="span_multi")]
		ISpanMultiTermQuery SpanMultiTerm { get; set; }

		[DataMember(Name ="span_near")]
		ISpanNearQuery SpanNear { get; set; }

		[DataMember(Name ="span_not")]
		ISpanNotQuery SpanNot { get; set; }

		[DataMember(Name ="span_or")]
		ISpanOrQuery SpanOr { get; set; }

		[DataMember(Name ="span_term")]
		ISpanTermQuery SpanTerm { get; set; }

		[DataMember(Name ="span_within")]
		ISpanWithinQuery SpanWithin { get; set; }

		void Accept(IQueryVisitor visitor);
	}

	public class SpanQuery : ISpanQuery
	{
		public bool IsStrict { get; set; }
		public bool IsVerbatim { get; set; }
		public bool IsWritable => IsVerbatim || !IsConditionless(this);
		public ISpanContainingQuery SpanContaining { get; set; }
		public ISpanFieldMaskingQuery SpanFieldMasking { get; set; }
		public ISpanFirstQuery SpanFirst { get; set; }
		public ISpanGapQuery SpanGap { get; set; }
		public ISpanMultiTermQuery SpanMultiTerm { get; set; }
		public ISpanNearQuery SpanNear { get; set; }
		public ISpanNotQuery SpanNot { get; set; }
		public ISpanOrQuery SpanOr { get; set; }
		public ISpanTermQuery SpanTerm { get; set; }
		public ISpanWithinQuery SpanWithin { get; set; }
		double? IQuery.Boost { get; set; }
		bool IQuery.Conditionless => IsConditionless(this);
		bool IQuery.IsWritable { get; }
		string IQuery.Name { get; set; }

		public void Accept(IQueryVisitor visitor) => new QueryWalker().Walk(this, visitor);

		internal static bool IsConditionless(ISpanQuery q) => new[]
		{
			q.SpanTerm as IQuery,
			q.SpanFirst,
			q.SpanNear,
			q.SpanOr,
			q.SpanNot,
			q.SpanMultiTerm,
			q.SpanGap,
			q.SpanFieldMasking
		}.All(sq => sq == null || sq.Conditionless);
	}

	public class SpanQueryDescriptor<T>
		: QueryDescriptorBase<SpanQueryDescriptor<T>, ISpanQuery>
			, ISpanQuery where T : class
	{
		protected override bool Conditionless => SpanQuery.IsConditionless(this);
		ISpanContainingQuery ISpanQuery.SpanContaining { get; set; }
		ISpanFieldMaskingQuery ISpanQuery.SpanFieldMasking { get; set; }
		ISpanFirstQuery ISpanQuery.SpanFirst { get; set; }
		ISpanGapQuery ISpanQuery.SpanGap { get; set; }
		ISpanMultiTermQuery ISpanQuery.SpanMultiTerm { get; set; }
		ISpanNearQuery ISpanQuery.SpanNear { get; set; }
		ISpanNotQuery ISpanQuery.SpanNot { get; set; }
		ISpanOrQuery ISpanQuery.SpanOr { get; set; }
		ISpanTermQuery ISpanQuery.SpanTerm { get; set; }
		ISpanWithinQuery ISpanQuery.SpanWithin { get; set; }

		void ISpanQuery.Accept(IQueryVisitor visitor) => new QueryWalker().Walk(this, visitor);

		public SpanQueryDescriptor<T> SpanTerm(Func<SpanTermQueryDescriptor<T>, ISpanTermQuery> selector) =>
			Assign(a => a.SpanTerm = selector?.Invoke(new SpanTermQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanFirst(Func<SpanFirstQueryDescriptor<T>, ISpanFirstQuery> selector) =>
			Assign(a => a.SpanFirst = selector?.Invoke(new SpanFirstQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanNear(Func<SpanNearQueryDescriptor<T>, ISpanNearQuery> selector) =>
			Assign(a => a.SpanNear = selector?.Invoke(new SpanNearQueryDescriptor<T>()));

		public SpanQueryDescriptor<T> SpanGap(Func<SpanGapQueryDescriptor<T>, ISpanGapQuery> selector) =>
			Assign(a => a.SpanGap = selector?.Invoke(new SpanGapQueryDescriptor<T>()));

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

		public SpanQueryDescriptor<T> SpanFieldMasking(Func<SpanFieldMaskingQueryDescriptor<T>, ISpanFieldMaskingQuery> selector) =>
			Assign(a => a.SpanFieldMasking = selector?.Invoke(new SpanFieldMaskingQueryDescriptor<T>()));
	}
}
