using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IndexState>))]
	public interface IIndexState
	{
		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }

		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }

		[JsonProperty("mappings")]
		IMappings Mappings { get; set; }
	}

	public class IndexState : IIndexState
	{
		public IIndexSettings Settings { get; set; }

		public IMappings Mappings { get; set; }

		public IAliases Aliases { get; set; }
	}
}
