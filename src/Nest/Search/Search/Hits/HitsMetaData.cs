using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class HitsMetaData<T> where T : class
	{
		[JsonProperty("total")]
		public long Total { get; internal set; }

		[JsonProperty("max_score")]
		public double MaxScore { get; internal set; }

		[JsonProperty("hits")]
		public IReadOnlyCollection<IHit<T>> Hits { get; internal set; } = EmptyReadOnly<IHit<T>>.Collection;


	}
}
