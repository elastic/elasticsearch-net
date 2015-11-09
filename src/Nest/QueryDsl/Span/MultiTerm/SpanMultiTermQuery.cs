using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SpanMultiTermQuery>))]
	public interface ISpanMultiTermQuery : ISpanSubQuery
	{
		[JsonProperty("match")]
		QueryContainer Match { get; set; }
	}

	public class SpanMultiTermQuery : QueryBase, ISpanMultiTermQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public QueryContainer Match { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.SpanMultiTerm = this;
		internal static bool IsConditionless(ISpanMultiTermQuery q) => q.Match == null || q.Match.IsConditionless;
	}

	public class SpanMultiTermQueryDescriptor<T> 
		: QueryDescriptorBase<SpanMultiTermQueryDescriptor<T>, ISpanMultiTermQuery>
		, ISpanMultiTermQuery
		where T : class
	{
		bool IQuery.Conditionless => SpanMultiTermQuery.IsConditionless(this);
		QueryContainer ISpanMultiTermQuery.Match { get; set; }

		public SpanMultiTermQueryDescriptor<T> Match(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.Match = selector(new QueryContainerDescriptor<T>()));
	}
}
