using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(SpanTermQueryConverter))]
	public interface ISpanTermQuery : ITermQuery, ISpanSubQuery
	{
	}
	
	public class SpanTermQuery : FieldNameQuery, ISpanTermQuery
	{
		bool IQuery.Conditionless => TermQuery.IsConditionless(this);
		public object Value { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.SpanTerm = this;
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanTermQueryDescriptor<T> : TermQueryDescriptorBase<SpanTermQueryDescriptor<T>, T>, ISpanTermQuery
		where T : class
	{
	}
}
