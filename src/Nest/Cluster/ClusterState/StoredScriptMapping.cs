using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class StoredScriptMapping
	{
		[JsonProperty("lang")]
		public string Language { get; internal set; }

		[JsonProperty("source")]
		public string Source { get; internal set; }

		[JsonProperty("options")]
		public IReadOnlyDictionary<string, string> Options { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;
	}
}
