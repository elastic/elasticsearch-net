using System;
using System.Collections.Generic;
using System.Linq;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPrefixQuery : ITermQuery
	{
		[JsonIgnore]
		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty("rewrite")]
		MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }
	}

	public class PrefixQuery : PlainQuery, IPrefixQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Prefix = this;
		}

		public string Name { get; set; }

		bool IQuery.IsConditionless { get { return false; } }
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }
		public object Value { get; set; }
		public double? Boost { get; set; }
		[Obsolete("Use MultiTermQueryRewrite")]
		public RewriteMultiTerm? Rewrite
		{
			get { return MultiTermQueryRewrite?.Rewrite; }
			set { MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value); }
		}
		public MultiTermQueryRewrite MultiTermQueryRewrite { get; set; }
	}

	public class PrefixQueryDescriptor<T> : TermQueryDescriptorBase<PrefixQueryDescriptor<T>, T>, 
		IPrefixQuery where T : class
	{
		protected IPrefixQuery Self { get { return this; } }

		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? IPrefixQuery.Rewrite
		{
			get { return Self.MultiTermQueryRewrite?.Rewrite; }
			set { Self.MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value); }
		}
		MultiTermQueryRewrite IPrefixQuery.MultiTermQueryRewrite { get; set; }

		[Obsolete("Use overload that accepts MultiTermQueryRewrite")]
		public PrefixQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			Self.MultiTermQueryRewrite = new MultiTermQueryRewrite(rewrite);
			return this;
		}

		public PrefixQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite)
		{
			Self.MultiTermQueryRewrite = rewrite;
			return this;
		}
	}
}
