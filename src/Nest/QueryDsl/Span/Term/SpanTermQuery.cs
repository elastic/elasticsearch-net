using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<SpanTermQuery, ISpanTermQuery>))]
	public interface ISpanTermQuery : ITermQuery, ISpanSubQuery { }

	[DataContract]
	public class SpanTermQuery : FieldNameQueryBase, ISpanTermQuery
	{
		public object Value { get; set; }

		protected override bool Conditionless => TermQuery.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanTerm = this;
	}

	public class SpanTermQueryDescriptor<T> : TermQueryDescriptorBase<SpanTermQueryDescriptor<T>, ISpanTermQuery, T>, ISpanTermQuery
		where T : class { }
}
