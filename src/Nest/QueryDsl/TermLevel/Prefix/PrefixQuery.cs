using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FieldNameQueryJsonConverter<PrefixQuery>))]
	public interface IPrefixQuery : ITermQuery
	{
		[JsonProperty("rewrite")]
		MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }

		[JsonIgnore]
		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	public class PrefixQuery : FieldNameQueryBase, IPrefixQuery
	{
		public MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }

		[Obsolete("Use MultiTermQueryRewrite")]
		public RewriteMultiTerm? Rewrite
		{
			get => MultiTermQueryRewrite?.Rewrite;
			set => MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		public object Value { get; set; }
		protected override bool Conditionless => TermQuery.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Prefix = this;
	}

	public class PrefixQueryDescriptor<T>
		: TermQueryDescriptorBase<PrefixQueryDescriptor<T>, T>,
			IPrefixQuery where T : class
	{
		protected new IPrefixQuery Self => this;

		MultiTermQueryRewrite IPrefixQuery.MultiTermQueryRewrite { get; set; }

		RewriteMultiTerm? IPrefixQuery.Rewrite
		{
			get => Self.MultiTermQueryRewrite?.Rewrite;
			set => Self.MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		[Obsolete("Use Rewrite(MultiTermQueryRewrite rewrite)")]
		public PrefixQueryDescriptor<T> Rewrite(RewriteMultiTerm? rewrite)
		{
			Self.Rewrite = rewrite;
			return this;
		}

		public PrefixQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite)
		{
			Self.MultiTermQueryRewrite = rewrite;
			return this;
		}
	}
}
