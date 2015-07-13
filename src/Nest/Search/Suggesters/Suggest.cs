using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Suggest
	{
		[JsonProperty("length")]
		public int Length { get; internal set; }
		[JsonProperty("offset")]
		public int Offset { get; internal set; }
		[JsonProperty("text")]
		public string Text { get; internal set; }
		[JsonProperty("options")]
		public IEnumerable<SuggestOption> Options { get; internal set; }

		public IEnumerable<CompletionOption<T>> GetCompletionOptions<T>()
		{
			return Options?.Select(x => new CompletionOption<T>(x)) ?? Enumerable.Empty<CompletionOption<T>>();
		}
	}
}
