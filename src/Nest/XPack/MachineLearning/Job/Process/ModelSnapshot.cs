using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class ModelSnapshot
	{
		/// <summary>
		/// An optional description of the job.
		/// </summary>
		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		/// <summary>
		/// The unique identifier for the job
		/// </summary>
		[DataMember(Name ="job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The timestamp of the latest processed record.
		/// </summary>
		[DataMember(Name ="latest_record_time_stamp")]
		[JsonFormatter(typeof(EpochMillisecondsNullableDateTimeOffsetFormatter))]
		public DateTimeOffset? LatestRecordTimeStamp { get; internal set; }

		/// <summary>
		/// The timestamp of the latest bucket result.
		/// </summary>
		[DataMember(Name ="latest_result_time_stamp")]
		[JsonFormatter(typeof(EpochMillisecondsNullableDateTimeOffsetFormatter))]
		public DateTimeOffset? LatestResultTimeStamp { get; internal set; }

		/// <summary>
		/// Summary information describing the model.
		/// </summary>
		[DataMember(Name ="model_size_stats")]
		public ModelSizeStats ModelSizeStats { get; internal set; }

		/// <summary>
		/// If true, this snapshot will not be deleted during automatic cleanup of snapshots older than
		/// the model snapshot retention days. However, this snapshot will be deleted when the
		/// job is deleted. The default value is false.
		/// </summary>
		[DataMember(Name ="retain")]
		public bool Retain { get; internal set; }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[DataMember(Name ="snapshot_doc_count")]
		public long SnapshotDocCount { get; internal set; }

		/// <summary>
		/// A numerical character string that uniquely identifies the model snapshot.
		/// </summary>
		[DataMember(Name ="snapshot_id")]
		public string SnapshotId { get; internal set; }

		/// <summary>
		/// The creation timestamp for the snapshot.
		/// </summary>
		[DataMember(Name ="timestamp")]
		[JsonFormatter(typeof(EpochMillisecondsDateTimeOffsetFormatter))]
		public DateTimeOffset Timestamp { get; internal set; }
	}
}
