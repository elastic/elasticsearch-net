using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class RecoveryStatus
	{
		[JsonProperty("shards")]
		public IEnumerable<ShardRecovery> Shards { get; internal set; }
	}
}