// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<SpanTermQuery, ISpanTermQuery>))]
	public interface ISpanTermQuery : ISpanSubQuery, IFieldNameQuery
	{
		[DataMember(Name = "value")]
		[JsonFormatter(typeof(SourceWriteFormatter<object>))]
		object Value { get; set; }
	}

	[DataContract]
	public class SpanTermQuery : FieldNameQueryBase, ISpanTermQuery
	{
		public object Value { get; set; }
		
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanTerm = this;

		internal static bool IsConditionless(ISpanTermQuery q) => q.Value == null || q.Value.ToString().IsNullOrEmpty() || q.Field.IsConditionless();
	}

	public class SpanTermQueryDescriptor<T> : FieldNameQueryDescriptorBase<SpanTermQueryDescriptor<T>, ISpanTermQuery, T>, ISpanTermQuery
		where T : class
	{
		protected override bool Conditionless => SpanTermQuery.IsConditionless(this);
		
		object ISpanTermQuery.Value { get; set; }

		public SpanTermQueryDescriptor<T> Value(object value) =>
			Assign(value, (a, v) => a.Value = v);
	}
}
