using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IWildcardQuery : ITermQuery, IFieldNameQuery
	{
		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }
	}

	public class WildcardQueryDescriptor<T> : 
		TermQueryDescriptorBase<WildcardQueryDescriptor<T>, T>, 
		IWildcardQuery 
		where T : class
	{
		RewriteMultiTerm? IWildcardQuery.Rewrite { get; set; }

		public WildcardQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IWildcardQuery)this).Rewrite = rewrite;
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
