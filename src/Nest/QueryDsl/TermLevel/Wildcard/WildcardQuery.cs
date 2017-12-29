using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryJsonConverter<WildcardQuery>))]
	public interface IWildcardQuery : ITermQuery
	{
		[JsonProperty("rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }
	}

	public class WildcardQuery<T> : WildcardQuery
		where T : class
	{
		public WildcardQuery(Expression<Func<T, object>> field) { this.Field = field; }
	}

	public class WildcardQuery : FieldNameQueryBase, IWildcardQuery
	{
		protected override bool Conditionless => TermQuery.IsConditionless(this);
		public object Value { get; set; }

		public MultiTermQueryRewrite Rewrite { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Wildcard = this;
	}

	public class WildcardQueryDescriptor<T> : TermQueryDescriptorBase<WildcardQueryDescriptor<T>, IWildcardQuery, T>,
		IWildcardQuery
		where T : class
	{
		MultiTermQueryRewrite IWildcardQuery.Rewrite { get; set; }

		public WildcardQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(a => a.Rewrite = rewrite);
		}
	}
