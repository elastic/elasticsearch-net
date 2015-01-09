using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISpanMultiTermQuery : ISpanSubQuery
	{
		[JsonProperty("match")]
		IQueryContainer Match { get; set; }
	}

	public class SpanMultiTermQuery : PlainQuery, ISpanMultiTermQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanMultiTerm = this;
		}

		public IQueryContainer Match { get; set; }

		public bool IsConditionless { get { return false; } }
	}

	public class SpanMultiTermQueryDescriptor<T> : ISpanMultiTermQuery
		where T : class
	{
		IQueryContainer ISpanMultiTermQuery.Match { get; set; }

		bool IQuery.IsConditionless { get { return false; } }

		public SpanMultiTermQueryDescriptor<T> Match(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((ISpanMultiTermQuery)this).Match = querySelector(q);
			return this;
		}
	}
}
