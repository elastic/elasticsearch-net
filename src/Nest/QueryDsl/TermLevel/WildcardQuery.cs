using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IWildcardQuery : ITermQuery
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
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
		public PropertyPathMarker Field { get; set; }
		public object Value { get; set; }
		public double? Boost { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Wildcard = this;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}
	}

	public class WildcardQueryDescriptor<T> : 
		TermQueryDescriptorBase<WildcardQueryDescriptor<T>, T>, 
		IWildcardQuery 
		where T : class
	{
		private IWildcardQuery Self { get { return this; } }

		RewriteMultiTerm? IWildcardQuery.Rewrite { get; set; }

		public WildcardQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			Self.Rewrite = rewrite;
			return this;
		}

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return Self.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}
	}
}
