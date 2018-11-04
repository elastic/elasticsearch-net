using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITemplateMapping
	{
		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }

		[JsonProperty("mappings")]
		IMappings Mappings { get; set; }

		[JsonProperty("order")]
		int? Order { get; set; }

		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }

		[Obsolete("Removed in NEST 6.x.")]
		[JsonProperty("template")]
		string Template { get; set; }

		[JsonProperty("version")]
		int? Version { get; set; }
	}

	public class TemplateMapping : ITemplateMapping
	{
		public IAliases Aliases { get; set; }

		public IMappings Mappings { get; set; }

		public int? Order { get; set; }

		public IIndexSettings Settings { get; set; }

		[Obsolete("Removed in NEST 6.x.")]
		public string Template { get; set; }

		public int? Version { get; set; }
	}
}
