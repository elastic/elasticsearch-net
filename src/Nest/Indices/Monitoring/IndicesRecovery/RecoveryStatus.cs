using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class RecoveryStatus
	{
		[JsonProperty("shards")]
		public IReadOnlyCollection<ShardRecovery> Shards { get; internal set; } =
			EmptyReadOnly<ShardRecovery>.Collection;
	}
}
