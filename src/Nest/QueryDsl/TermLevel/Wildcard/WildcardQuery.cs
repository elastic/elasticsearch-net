using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FieldNameQueryJsonConverter<WildcardQuery>))]
	public interface IWildcardQuery : ITermQuery
	{
		[JsonProperty("rewrite")]
		MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }

		[JsonIgnore]
		[Obsolete("Use MultiTermQueryRewrite, Fixed in NEST 6.x")]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	public class WildcardQuery<T> : WildcardQuery
		where T : class
	{
		public WildcardQuery(Expression<Func<T, object>> field) => Field = field;
	}

	public class WildcardQuery : FieldNameQueryBase, IWildcardQuery
	{
		public MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }

		[Obsolete("Use MultiTermQueryRewrite, Fixed in NEST 6.x")]
		public RewriteMultiTerm? Rewrite
		{
			get => MultiTermQueryRewrite?.Rewrite;
			set => MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		public object Value { get; set; }
		protected override bool Conditionless => TermQuery.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Wildcard = this;
	}

	public class WildcardQueryDescriptor<T>
		: TermQueryDescriptorBase<WildcardQueryDescriptor<T>, T>,
			IWildcardQuery
		where T : class
	{
		protected new IWildcardQuery Self => this;

		MultiTermQueryRewrite IWildcardQuery.MultiTermQueryRewrite { get; set; }

		RewriteMultiTerm? IWildcardQuery.Rewrite
		{
			get => Self.MultiTermQueryRewrite?.Rewrite;
			set => Self.MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		[Obsolete("Use MultiTermQueryRewrite, Fixed in NEST 6.x")]
		public WildcardQueryDescriptor<T> Rewrite(RewriteMultiTerm? rewrite)
		{
			Self.MultiTermQueryRewrite = rewrite != null
				? new MultiTermQueryRewrite(rewrite.Value)
				: null;
			return this;
		}

		public WildcardQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite)
		{
			Self.MultiTermQueryRewrite = rewrite;
			return this;
		}
	}
}
