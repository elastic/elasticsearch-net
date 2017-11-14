using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(FieldNameQueryJsonConverter<SpanTermQuery>))]
	public interface ISpanTermQuery : ITermQuery, ISpanSubQuery
	{
	}

	public class SpanTermQuery : FieldNameQueryBase, ISpanTermQuery
	{
		protected override bool Conditionless => TermQuery.IsConditionless(this);
		public object Value { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanTerm = this;
	}

	public class SpanTermQueryDescriptor<T> : TermQueryDescriptorBase<SpanTermQueryDescriptor<T>, ISpanTermQuery, T>, ISpanTermQuery
		where T : class
	{
	}
}
