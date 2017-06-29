using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryJsonConverter<WildcardQuery>))]
	public interface IWildcardQuery : ITermQuery
	{
		[JsonIgnore]
		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty("rewrite")]
		MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }
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

		[Obsolete("Use MultiTermQueryRewrite")]
		public RewriteMultiTerm? Rewrite
		{
			get => MultiTermQueryRewrite?.Rewrite;
			set => MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		public MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Wildcard = this;
	}

	public class WildcardQueryDescriptor<T> : TermQueryDescriptorBase<WildcardQueryDescriptor<T>, T>,
		IWildcardQuery
		where T : class
	{
		protected new IWildcardQuery Self => this;

		RewriteMultiTerm? IWildcardQuery.Rewrite
		{
			get => Self.MultiTermQueryRewrite?.Rewrite;
			set => Self.MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		MultiTermQueryRewrite IWildcardQuery.MultiTermQueryRewrite { get; set; }

		[Obsolete("Use Rewrite(MultiTermQueryRewrite rewrite)")]
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
