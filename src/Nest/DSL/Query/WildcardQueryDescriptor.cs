using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
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
		public WildcardQuery(Expression<Func<T, object>> field)
		{
			this.Field = field;
		}
	}
	public class WildcardQuery : PlainQuery, IWildcardQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		PropertyPathMarker IFieldNameQuery.GetFieldName() { return this.Field; }

		void IFieldNameQuery.SetFieldName(string fieldName) { this.Field = fieldName; }

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

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Wildcard = this;
		}
	}

	public class WildcardQueryDescriptor<T> : 
		TermQueryDescriptorBase<WildcardQueryDescriptor<T>, T>, 
		IWildcardQuery 
		where T : class
	{
		protected IWildcardQuery Self { get { return this; } }

		[Obsolete("Use MultiTermQueryRewrite")]
		RewriteMultiTerm? IWildcardQuery.Rewrite
		{
			get { return Self.MultiTermQueryRewrite?.Rewrite; }
			set { Self.MultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value); }
		}
		MultiTermQueryRewrite IWildcardQuery.MultiTermQueryRewrite { get; set; }

		[Obsolete("Use overload that accepts MultiTermQueryRewrite")]
		public WildcardQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			Self.MultiTermQueryRewrite = new MultiTermQueryRewrite(rewrite);
			return this;
		}

		public WildcardQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite)
		{
			Self.MultiTermQueryRewrite = rewrite;
			return this;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IWildcardQuery)this).Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IWildcardQuery)this).Field = fieldName;
		}
	}
}
