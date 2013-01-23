using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	//	[JsonConverter(typeof(TemplateMappingConverter))]
	//	[JsonObject(MemberSerialization.OptIn)]
	public class TemplateMapping
	{
		[JsonProperty("template")]
		public string Template { get; set; }

		[JsonProperty("order")]
		public int Order { get; set; }

		[JsonProperty("settings")]
		public TemplateIndexSettings Settings { get; set; }

		[JsonProperty("mappings")]
		public Dictionary<string, RootObjectMapping> Mappings { get; set; }
	}
}