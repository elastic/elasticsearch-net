using System;
using Newtonsoft.Json;

namespace Nest
{
	public class ShardRecovery
	{
		[JsonProperty("id")]
		public long Id { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("stage")]
		public string Stage { get; internal set; }

		[JsonProperty("primary")]
		public bool Primary { get; internal set; }

		[JsonProperty("start_time")]
		public DateTime? StartTime { get; internal set; }

		[JsonProperty("stop_time")]
		public DateTime? StopTime { get; internal set; }

		[JsonProperty("total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }

		[JsonProperty("source")]
		public RecoveryOrigin Source { get; internal set; }

		[JsonProperty("target")]
		public RecoveryOrigin Target { get; internal set; }

		[JsonProperty("index")]
		public RecoveryIndexStatus Index { get; internal set; }

		[JsonProperty("translog")]
		public RecoveryTranslogStatus Translog { get; internal set; }
		
		[JsonProperty("start")]
		public RecoveryStartStatus Start { get; internal set; }
	}
}