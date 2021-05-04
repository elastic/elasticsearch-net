// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SpanFieldMaskingQuery))]
	public interface ISpanFieldMaskingQuery : ISpanSubQuery
	{
		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name = "query")]
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

		public SpanFieldMaskingQueryDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public SpanFieldMaskingQueryDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public SpanFieldMaskingQueryDescriptor<T> Query(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new SpanQueryDescriptor<T>()));
	}
}
