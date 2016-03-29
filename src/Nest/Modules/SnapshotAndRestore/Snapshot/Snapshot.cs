using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class Snapshot
	{
		[JsonProperty("snapshot")]
		public string Name { get; internal set;  }

		[JsonProperty("indices")]
		public IEnumerable<IndexName> Indices { get; internal set; }

		[JsonProperty("state")]
		public string State { get; internal set; }

		[JsonProperty("start_time")]
		public DateTime StartTime { get; internal set;  }
		
		[JsonProperty("start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set;  }
		
		[JsonProperty("end_time")]
		public DateTime EndTime { get; internal set;  }
		
		[JsonProperty("end_time_in_millis")]
		public long EndTimeInMilliseconds { get; internal set;  }
		
		[JsonProperty("duration_in_millis")]
		public long DurationInMilliseconds { get; internal set;  }

		[JsonProperty("shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty("failures")]
		public IEnumerable<SnapshotShardFailure> ShardFailures { get; internal set; }

		/// <summary>
		/// Contains the reason for each shard failure.
		/// </summary>
		/// For 2.0, remove this and rename ShardFailures => Failures
		[JsonIgnore]
		public IEnumerable<string> Failures
		{
			get
			{
				if (this.ShardFailures != null) 
					return this.ShardFailures.Select(f => f.Reason);
				return new List<string>();
			}
		}
		
	}
}