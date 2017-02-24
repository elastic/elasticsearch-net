using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class ShardProfile
	{
		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("searches")]
		public IReadOnlyCollection<SearchProfile> Searches { get; internal set; } =
			EmptyReadOnly<SearchProfile>.Collection;

	}
}
