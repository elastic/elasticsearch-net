using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ModelSnapshot
	{
		/// <summary>
		/// The unique identifier for the job
		/// </summary>
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The creation timestamp for the snapshot.
		/// </summary>
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		/// <summary>
		/// An optional description of the job.
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; internal set; }

		/// <summary>
		/// A numerical character string that uniquely identifies the model snapshot.
		/// </summary>
		[JsonProperty("snapshot_id")]
		public string SnapshotId { get; internal set; }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[JsonProperty("snapshot_doc_count")]
		public long SnapshotDocCount { get; internal set; }

		/// <summary>
		/// Summary information describing the model.
		/// </summary>
		[JsonProperty("model_size_stats")]
		public ModelSizeStats ModelSizeStats { get; internal set; }

		/// <summary>
		/// The timestamp of the latest processed record.
		/// </summary>
		[JsonProperty("latest_record_time_stamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset? LatestRecordTimeStamp{ get; internal set; }

		/// <summary>
		/// The timestamp of the latest bucket result.
		/// </summary>
		[JsonProperty("latest_result_time_stamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset? LatestResultTimeStamp{ get; internal set; }

		/// <summary>
		/// If true, this snapshot will not be deleted during automatic cleanup of snapshots older than
		/// the model snapshot retention days. However, this snapshot will be deleted when the
		/// job is deleted. The default value is false.
		/// </summary>
		[JsonProperty("retain")]
		public bool Retain{ get; internal set; }
	}
}
