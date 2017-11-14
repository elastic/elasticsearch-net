using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class HitsMetadata<T> where T : class
	{
		[JsonProperty("total")]
		public long Total { get; internal set; }

		[JsonProperty("max_score")]
		public double MaxScore { get; internal set; }

		[JsonProperty("hits")]
		public IReadOnlyCollection<IHit<T>> Hits { get; internal set; } = EmptyReadOnly<IHit<T>>.Collection;


	}
}
