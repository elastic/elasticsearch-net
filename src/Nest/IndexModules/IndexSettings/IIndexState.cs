using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndexState
	{
		[JsonProperty("settings")]
		IIndexSettings Settings { get; set; }

		[JsonProperty("aliases")]
		IAliases Aliases { get; set; }

		[JsonProperty("warmers")]
		IWarmers Warmers { get; set; }

		[JsonProperty("mappings")]
		IMappings Mappings { get; set; }

		[JsonProperty("similarity")]
		ISimilarities Similarity { get; set; }
	}
}