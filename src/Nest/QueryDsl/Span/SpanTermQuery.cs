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
		bool IQuery.Conditionless { get { return false; } }
		public object Value { get; set; }
		public double? Boost { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanTerm = this;
		}
	}


	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanTermQueryDescriptor<T> : TermQueryDescriptorBase<SpanTermQueryDescriptor<T>, T>, ISpanTermQuery
		where T : class
	{
	}
}
