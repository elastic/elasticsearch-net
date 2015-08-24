using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	//	[JsonConverter(typeof(TemplateMappingConverter))]
	//	[JsonObject(MemberSerialization.OptIn)]
	public class TemplateMapping
	{
		public TemplateMapping()
		{
		}

		[JsonProperty("template")]
		public string Template { get; set; }

		[JsonProperty("order")]
		public int? Order { get; set; }

		[JsonProperty("settings")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public FluentDictionary<string, object> Settings { get; set; }

		[JsonProperty("mappings")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, ITypeMapping> Mappings { get; set; }

		[JsonProperty("warmers")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, WarmerMapping> Warmers { get; set; }
		
		[JsonProperty("aliases")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, ICreateAliasOperation> Aliases { get; set; }
	}
}