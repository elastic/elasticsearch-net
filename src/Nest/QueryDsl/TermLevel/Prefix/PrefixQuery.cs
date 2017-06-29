using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryJsonConverter<PrefixQuery>))]
	public interface IPrefixQuery : ITermQuery
	{
		[JsonIgnore]
		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty("rewrite")]
		MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }
	}

	public class PrefixQuery : FieldNameQueryBase, IPrefixQuery
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

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Prefix = this;
	}

	public class PrefixQueryDescriptor<T> : TermQueryDescriptorBase<PrefixQueryDescriptor<T>, T>,
		IPrefixQuery where T : class
	{
		protected new IPrefixQuery Self => this;

		RewriteMultiTerm? IPrefixQuery.Rewrite
		{
			get => Self.MultiTermQueryRewrite?.Rewrite;
			set => Self.MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		MultiTermQueryRewrite IPrefixQuery.MultiTermQueryRewrite { get; set; }

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
