using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<WildcardQuery, IWildcardQuery>))]
	public interface IWildcardQuery : ITermQuery
	{
		[DataMember(Name = "rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }
	}

	public class WildcardQuery<T> : WildcardQuery
		where T : class
	{
		public WildcardQuery(Expression<Func<T, object>> field) => Field = field;
	}

	public class WildcardQuery : FieldNameQueryBase, IWildcardQuery
	{
		public MultiTermQueryRewrite Rewrite { get; set; }
		public object Value { get; set; }
		protected override bool Conditionless => TermQuery.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Wildcard = this;
	}

	public class WildcardQueryDescriptor<T>
		: TermQueryDescriptorBase<WildcardQueryDescriptor<T>, IWildcardQuery, T>,
			IWildcardQuery
		where T : class
	{
		MultiTermQueryRewrite IWildcardQuery.Rewrite { get; set; }

		public WildcardQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(a => a.Rewrite = rewrite);
	}
}
