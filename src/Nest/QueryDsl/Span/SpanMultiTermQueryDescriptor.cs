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

	public class SpanMultiTermQuery : PlainQuery, ISpanMultiTermQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanMultiTerm = this;
		}

		public IQueryContainer Match { get; set; }

		public string Name { get; set; }

		//TODO change to explicit IQuery implenentation in 2.0?
		public bool IsConditionless { get { return false; } }
	}

	public class SpanMultiTermQueryDescriptor<T> : ISpanMultiTermQuery
		where T : class
	{
		private ISpanMultiTermQuery Self { get { return this; } }
		IQueryContainer ISpanMultiTermQuery.Match { get; set; }

		bool IQuery.IsConditionless { get { return false; } }
		string IQuery.Name { get; set; }

		public SpanMultiTermQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		
		public SpanMultiTermQueryDescriptor<T> Match(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			Self.Match = querySelector(q);
			return this;
		}
	}
}
