using System.Collections.Generic;
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
		public int? Order { get; set; }

		[JsonProperty("settings")]
		public IIndexSettings Settings { get; set; }

		[JsonProperty("mappings")]
		public IMappings Mappings { get; set; }

		[JsonProperty("warmers")]
		public IWarmers Warmers { get; set; }
		
		[JsonProperty("aliases")]
		public IAliases Aliases { get; set; }
	}
}