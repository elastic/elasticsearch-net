using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetIndexTemplateResponse : IResponse
	{
		IReadOnlyDictionary<string, TemplateMapping> TemplateMappings { get; }
	}

	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetIndexTemplateResponse, string, TemplateMapping>))]
	public class GetIndexTemplateResponse : DictionaryResponseBase<string, TemplateMapping>, IGetIndexTemplateResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, TemplateMapping> TemplateMappings => Self.BackingDictionary;
	}
}
