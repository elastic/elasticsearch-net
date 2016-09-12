using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SpanFieldMaskingQueryDescriptor<object>>))]
	public interface ISpanFieldMaskingQuery : ISpanSubQuery
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("query")]
		ISpanQuery Query { get; set; }
	}

	public class SpanFieldMaskingQuery : QueryBase, ISpanFieldMaskingQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public ISpanQuery Query { get; set; }
		public Field Field { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanFieldMasking = this;
		internal static bool IsConditionless(ISpanFieldMaskingQuery q) =>
			q.Field.IsConditionless() || q.Query == null || q.Query.Conditionless;
	}

	public class SpanFieldMaskingQueryDescriptor<T>
		: QueryDescriptorBase<SpanFieldMaskingQueryDescriptor<T>, ISpanFieldMaskingQuery>
		, ISpanFieldMaskingQuery where T : class
	{
		protected override bool Conditionless => SpanFieldMaskingQuery.IsConditionless(this);
		ISpanQuery ISpanFieldMaskingQuery.Query { get; set; }
		Field ISpanFieldMaskingQuery.Field { get; set; }

		public SpanFieldMaskingQueryDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SpanFieldMaskingQueryDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public SpanFieldMaskingQueryDescriptor<T> Query(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(a => a.Query = selector?.Invoke(new SpanQueryDescriptor<T>()));
	}
}
