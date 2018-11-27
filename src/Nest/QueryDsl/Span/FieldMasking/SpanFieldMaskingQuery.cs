using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(SpanFieldMaskingQueryDescriptor<object>))]
	public interface ISpanFieldMaskingQuery : ISpanSubQuery
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="query")]
		ISpanQuery Query { get; set; }
	}

	public class SpanFieldMaskingQuery : QueryBase, ISpanFieldMaskingQuery
	{
		public Field Field { get; set; }
		public ISpanQuery Query { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanFieldMasking = this;

		internal static bool IsConditionless(ISpanFieldMaskingQuery q) =>
			q.Field.IsConditionless() || q.Query == null || q.Query.Conditionless;
	}

	public class SpanFieldMaskingQueryDescriptor<T>
		: QueryDescriptorBase<SpanFieldMaskingQueryDescriptor<T>, ISpanFieldMaskingQuery>
			, ISpanFieldMaskingQuery where T : class
	{
		protected override bool Conditionless => SpanFieldMaskingQuery.IsConditionless(this);
		Field ISpanFieldMaskingQuery.Field { get; set; }
		ISpanQuery ISpanFieldMaskingQuery.Query { get; set; }

		public SpanFieldMaskingQueryDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SpanFieldMaskingQueryDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public SpanFieldMaskingQueryDescriptor<T> Query(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(a => a.Query = selector?.Invoke(new SpanQueryDescriptor<T>()));
	}
}
