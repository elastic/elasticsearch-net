using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Provides information about the size and contents of the model.
	/// </summary>
	[DataContract]
	public class ModelSizeStats
	{
		/// <summary>
		/// The number of buckets for which new entities in incoming data were not processed due to
		/// insufficient model memory.
		/// </summary>
		[DataMember(Name = "bucket_allocation_failures_count")]
		public long BucketAllocationFailuresCount { get; internal set; }

		/// <summary>
		///  A unique identifier for the job.
		/// </summary>
		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The timestamp according to server time.
		/// </summary>
		[DataMember(Name = "log_time")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset LogTime { get; internal set; }

		/// <summary>
		/// The status of the mathematical models.
		/// </summary>
		[DataMember(Name = "memory_status")]
		public MemoryStatus MemoryStatus { get; internal set; }

		/// <summary>
		/// The number of bytes of memory used by the models. This is the maximum value since the last time the
		/// model was persisted. If the job is closed, this value indicates the latest size.
		/// </summary>
		[DataMember(Name = "model_bytes")]
		public long ModelBytes { get; internal set; }

		/// <summary>
		/// For internal use. The type of result.
		/// </summary>
		[DataMember(Name = "result_type")]
		public string ResultType { get; internal set; }

		/// <summary>
		/// The timestamp according to the timestamp of the data.
		/// </summary>
		[DataMember(Name = "timestamp")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? Timestamp { get; internal set; }

		/// <summary>
		/// The number of by field values that were analyzed by the models.
		/// </summary>
		[DataMember(Name = "total_by_field_count")]
		public long TotalByFieldCount { get; internal set; }

		/// <summary>
		/// The number of over field values that were analyzed by the models.
		/// </summary>
		[DataMember(Name = "total_over_field_count")]
		public long TotalOverFieldCount { get; internal set; }

		/// <summary>
		/// The number of partition field values that were analyzed by the models.
		/// </summary>
		[DataMember(Name = "total_partition_field_count")]
		public long TotalPartitionFieldCount { get; internal set; }
	}
}
