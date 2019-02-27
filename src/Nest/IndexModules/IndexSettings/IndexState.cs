using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IndexState>))]
	public interface IIndexState
	{
		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }

		[JsonProperty("mappings")]
		ITypeMapping Mappings { get; set; }

		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }
	}

	public class IndexState : IIndexState
	{
		public IAliases Aliases { get; set; }

		public ITypeMapping Mappings { get; set; }
		public IIndexSettings Settings { get; set; }
	}
}
