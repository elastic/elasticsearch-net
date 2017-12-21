using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITemplateMapping
	{
		[JsonProperty("index_patterns")]
		IReadOnlyCollection<string> IndexPatterns { get; set; }

		[JsonProperty("order")]
		int? Order { get; set; }

		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }

		[JsonProperty("mappings")]
		IMappings Mappings { get; set; }

		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }

		[JsonProperty("version")]
		int? Version { get; set; }
	}

	public class TemplateMapping : ITemplateMapping
	{
		public IReadOnlyCollection<string> IndexPatterns {get;set;} = EmptyReadOnly<string>.Collection;

		public int? Order { get; set; }

		public IIndexSettings Settings { get; set; }

		public IMappings Mappings { get; set; }

		public IAliases Aliases { get; set; }

		public int? Version { get; set; }
	}
}
