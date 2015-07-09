using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISpanMultiTermQuery : ISpanSubQuery
	{
		[JsonProperty("match")]
		IQueryContainer Match { get; set; }
	}

	public class SpanMultiTermQuery : QueryBase, ISpanMultiTermQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IQueryContainer Match { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.SpanMultiTerm = this;
		internal static bool IsConditionless(ISpanMultiTermQuery q) => q.Match == null || q.Match.IsConditionless;
	}

	public class SpanMultiTermQueryDescriptor<T> : ISpanMultiTermQuery
		where T : class
	{
		private ISpanMultiTermQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => SpanMultiTermQuery.IsConditionless(this);
		IQueryContainer ISpanMultiTermQuery.Match { get; set; }

		public SpanMultiTermQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		
		public SpanMultiTermQueryDescriptor<T> Match(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryContainerDescriptor<T>();
			Self.Match = querySelector(q);
			return this;
		}
	}
}
