using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class ShardProfile
	{
		[JsonProperty("aggregations")]
		public IReadOnlyCollection<AggregationProfile> Aggregations { get; internal set; } =
			EmptyReadOnly<AggregationProfile>.Collection;

		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("searches")]
		public IReadOnlyCollection<SearchProfile> Searches { get; internal set; } =
			EmptyReadOnly<SearchProfile>.Collection;
	}
}
