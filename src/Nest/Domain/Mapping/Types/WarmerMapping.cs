using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	//	[JsonConverter(typeof(TemplateMappingConverter))]
	//	[JsonObject(MemberSerialization.OptIn)]
	public class WarmerMapping
	{
		public WarmerMapping()
		{
			this.Mappings = new Dictionary<string, RootObjectMapping>();
			this.Settings = new FluentDictionary<string, object>();
		}

		[JsonProperty("facets")]
		public string Template { get; set; }

		[JsonProperty("order")]
		public int Order { get; set; }

		[JsonProperty("settings")]
		public FluentDictionary<string, object> Settings { get; set; }

		[JsonProperty("mappings")]
		public Dictionary<string, RootObjectMapping> Mappings { get; set; }
	}
}