using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexTemplateResponse : IResponse
	{
		IDictionary<string, TemplateMapping> TemplateMappings { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetIndexTemplateResponse, string, TemplateMapping>))]
	public class GetIndexTemplateResponse : DictionaryResponseBase<string, TemplateMapping>, IGetIndexTemplateResponse
	{
		[JsonIgnore]
		public IDictionary<string, TemplateMapping> TemplateMappings => Self.BackingDictionary;
	}
}
