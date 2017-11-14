using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexTemplateResponse : IResponse
	{
		IReadOnlyDictionary<string, TemplateMapping> TemplateMappings { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetIndexTemplateResponse, string, TemplateMapping>))]
	public class GetIndexTemplateResponse : DictionaryResponseBase<string, TemplateMapping>, IGetIndexTemplateResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, TemplateMapping> TemplateMappings => Self.BackingDictionary;
	}
}
