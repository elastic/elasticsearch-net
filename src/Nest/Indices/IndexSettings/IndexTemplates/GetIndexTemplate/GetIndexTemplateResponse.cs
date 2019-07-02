using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetIndexTemplateResponse, string, TemplateMapping>))]
	public class GetIndexTemplateResponse : DictionaryResponseBase<string, TemplateMapping>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, TemplateMapping> TemplateMappings => Self.BackingDictionary;
	}
}
