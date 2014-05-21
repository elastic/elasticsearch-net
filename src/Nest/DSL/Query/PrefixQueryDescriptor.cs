using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPrefixQuery : ITermQuery
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	public class PrefixQuery : PlainQuery, IPrefixQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Prefix = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public PropertyPathMarker GetFieldName()
		{
			return this.Field;
		}

		public void SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }
		public object Value { get; set; }
		public double? Boost { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }
	}

	public class PrefixQueryDescriptor<T> : TermQueryDescriptorBase<PrefixQueryDescriptor<T>, T>, 
		IPrefixQuery where T : class
	{
		RewriteMultiTerm? IPrefixQuery.Rewrite { get; set; }

		public PrefixQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IPrefixQuery)this).Rewrite = rewrite;
			return this;
		}
		
	}
}
