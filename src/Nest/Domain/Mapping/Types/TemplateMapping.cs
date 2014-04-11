using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	//	[JsonConverter(typeof(TemplateMappingConverter))]
	//	[JsonObject(MemberSerialization.OptIn)]
	public class TemplateMapping
	{
		public TemplateMapping()
		{
			this.Mappings = new Dictionary<string, RootObjectMapping>();
			this.Warmers = new Dictionary<string, WarmerMapping>();
			this.Settings = new FluentDictionary<string, object>();
		}

		[JsonProperty("template")]
		public string Template { get; set; }

		[JsonProperty("order")]
		public int Order { get; set; }

		[JsonProperty("settings")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public FluentDictionary<string, object> Settings { get; set; }

		[JsonProperty("mappings")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, RootObjectMapping> Mappings { get; set; }

		[JsonProperty("warmers")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, WarmerMapping> Warmers { get; set; }
		
		[JsonProperty("aliases")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, CreateAliasDescriptor> Aliases { get; set; }
	}
}