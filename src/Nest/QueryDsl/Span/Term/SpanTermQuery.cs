using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(FieldNameQueryJsonConverter<SpanTermQuery>))]
	public interface ISpanTermQuery : ITermQuery, ISpanSubQuery { }

	public class SpanTermQuery : FieldNameQueryBase, ISpanTermQuery
	{
		public object Value { get; set; }
		protected override bool Conditionless => TermQuery.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanTerm = this;
	}

	[DataContract]
	public class SpanTermQueryDescriptor<T> : TermQueryDescriptorBase<SpanTermQueryDescriptor<T>, ISpanTermQuery, T>, ISpanTermQuery
		where T : class { }
}
