using System;
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

		[JsonIgnore]
		[Obsolete("Use Similarity within Settings. Removed in NEST 6.x")]
		ISimilarities Similarity { get; set; }
	}

	public class IndexState : IIndexState
	{
		public IIndexSettings Settings { get; set; }

		public IMappings Mappings { get; set; }

		public IAliases Aliases { get; set; }

		[Obsolete("Use Similarity within Settings. Removed in NEST 6.x")]
		public ISimilarities Similarity { get; set; }
	}
}
