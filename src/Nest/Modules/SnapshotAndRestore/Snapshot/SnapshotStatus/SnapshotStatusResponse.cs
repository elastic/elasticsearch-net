using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface ISnapshotStatusResponse : IResponse
	{
		[DataMember(Name ="snapshots")]
		IReadOnlyCollection<SnapshotStatus> Snapshots { get; }
	}

	[DataContract]
	public class SnapshotStatusResponse : ResponseBase, ISnapshotStatusResponse
	{
		[DataMember(Name ="snapshots")]
		public IReadOnlyCollection<SnapshotStatus> Snapshots { get; internal set; } = EmptyReadOnly<SnapshotStatus>.Collection;
	}

	public class SnapshotStatus
	{
		[DataMember(Name ="include_global_state")]
		public bool? IncludeGlobalState { get; internal set; }

		[DataMember(Name ="indices")]
		public IReadOnlyDictionary<string, SnapshotIndexStats> Indices { get; internal set; } = EmptyReadOnly<string, SnapshotIndexStats>.Dictionary;

		[DataMember(Name ="repository")]
		public string Repository { get; internal set; }

		[DataMember(Name ="shards_stats")]
		public SnapshotShardsStats ShardsStats { get; internal set; }

		[DataMember(Name ="snapshot")]
		public string Snapshot { get; internal set; }

		[DataMember(Name ="state")]
		public string State { get; internal set; }

		[DataMember(Name ="stats")]
		public SnapshotStats Stats { get; internal set; }

		[DataMember(Name ="uuid")]
		public string UUID { get; internal set; }
	}

	public class SnapshotIndexStats
	{
		[DataMember(Name ="shards")]
		public IReadOnlyDictionary<string, SnapshotShardsStats> Shards { get; internal set; } = EmptyReadOnly<string, SnapshotShardsStats>.Dictionary;

		[DataMember(Name ="shards_stats")]
		public SnapshotShardsStats ShardsStats { get; internal set; }

		[DataMember(Name ="stats")]
		public SnapshotStats Stats { get; internal set; }
	}

	public class SnapshotIndexShardStats
	{
		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="stage")]
		public string Stage { get; internal set; }

		[DataMember(Name ="stats")]
		public SnapshotStats Stats { get; internal set; }
	}

	public class SnapshotShardsStats
	{
		[DataMember(Name ="done")]
		public long Done { get; internal set; }

		[DataMember(Name ="failed")]
		public long Failed { get; internal set; }

		[DataMember(Name ="finalizing")]
		public long Finalizing { get; internal set; }

		[DataMember(Name ="initializing")]
		public long Initializing { get; internal set; }

		[DataMember(Name ="started")]
		public long Started { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }
	}

	public class SnapshotStats
	{
		[DataMember(Name ="number_of_files")]
		public long NumberOfFiles { get; internal set; }

		[DataMember(Name ="processed_files")]
		public long ProcessedFiles { get; internal set; }

		[DataMember(Name ="processed_size_in_bytes")]
		public long ProcessedSizeInBytes { get; internal set; }

		[DataMember(Name ="start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="time_in_millis")]
		public long TimeInMilliseconds { get; internal set; }

		[DataMember(Name ="total_size_in_bytes")]
		public long TotalSizeInBytes { get; internal set; }
	}
}
