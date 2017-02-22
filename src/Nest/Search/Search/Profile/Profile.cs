using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class Profile
	{
		[JsonProperty("shards")]
		public IReadOnlyCollection<ShardProfile> Shards { get; internal set; } =
			EmptyReadOnly<ShardProfile>.Collection;
	}
}
