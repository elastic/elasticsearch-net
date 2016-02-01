using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISnapshotStatusResponse : IResponse
	{
		[JsonProperty("snapshots")]
		IEnumerable<SnapshotStatus> Snapshots { get; set; }
	}

	[JsonObject]
	public class SnapshotStatusResponse : ResponseBase, ISnapshotStatusResponse
	{

		[JsonProperty("snapshots")]
		public IEnumerable<SnapshotStatus> Snapshots { get; set; }

	}

	public class SnapshotStatus
	{
		[JsonProperty("snapshot")]
		public string Snapshot { get; internal set; }
		[JsonProperty("repository")]
		public string Repository { get; internal set; }
		[JsonProperty("state")]
		public string State { get; internal set; }
		[JsonProperty("shards_stats")]
		public SnapshotShardsStats ShardsStats { get; internal set; }
		[JsonProperty("stats")]
		public SnapshotStats Stats { get; internal set; }
		[JsonProperty("indices")]
		public IDictionary<string, SnapshotIndexStats> Indices { get; internal set; } 
	}
	
	public class SnapshotIndexStats
	{
		[JsonProperty("shards_stats")]
		public SnapshotShardsStats ShardsStats { get; internal set; }
		[JsonProperty("stats")]
		public SnapshotStats Stats { get; internal set; }
		[JsonProperty("shards")]
		public IDictionary<string, SnapshotShardsStats> Shards { get; internal set; } 
	}

	public class SnapshotIndexShardStats
	{
		[JsonProperty("stage")]
		public string Stage { get; internal set; }
		[JsonProperty("node")]
		public string Node { get; internal set; }
		[JsonProperty("stats")]
		public SnapshotStats Stats { get; internal set; }
	}

	public class SnapshotShardsStats
	{
		[JsonProperty("initializing")]
		public long Initializing { get; internal set; }
		[JsonProperty("started")]
		public long Started { get; internal set; }
		[JsonProperty("finalizing")]
		public long Finalizing { get; internal set; }
		[JsonProperty("done")]
		public long Done { get; internal set; }
		[JsonProperty("failed")]
		public long Failed { get; internal set; }
		[JsonProperty("total")]
		public long Total { get; internal set; }
	}
	public class SnapshotStats
	{
		[JsonProperty("number_of_files")]
		public long NumberOfFiles { get; internal set; }
		[JsonProperty("processed_files")]
		public long ProcessedFiles { get; internal set; }
		[JsonProperty("total_size_in_bytes")]
		public long TotalSizeInBytes { get; internal set; }
		[JsonProperty("processed_size_in_bytes")]
		public long ProcessedSizeInBytes { get; internal set; }
		[JsonProperty("start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }
		[JsonProperty("time_in_millis")]
		public long TimeInMilliseconds { get; internal set; }
	}
}
