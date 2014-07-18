using System;
using System.Collections.Generic;
using System.Linq;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(SpanTermQueryConverter))]
	public interface ISpanTermQuery : ITermQuery, ISpanSubQuery
	{
		
	}
	
	public class SpanTermQuery : PlainQuery, ISpanTermQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanTerm = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }
		public object Value { get; set; }
		public double? Boost { get; set; }
	}


	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SpanTermQueryDescriptor<T> : TermQueryDescriptorBase<SpanTermQueryDescriptor<T>, T>, ISpanTermQuery
		where T : class
	{
	}
}
