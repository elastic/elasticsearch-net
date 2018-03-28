using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The Post Job Data API response
	/// </summary>
	public interface IPostJobDataResponse : IResponse
	{
		/// <summary>
		/// The unique identifier for the job.
		/// </summary>
		[JsonProperty("job_id")]
		string JobId { get; }

		/// <summary>
		/// The count of processed records.
		/// </summary>
		[JsonProperty("processed_record_count")]
		long ProcessedRecordCount { get; }

		/// <summary>
		/// The count of processed fields.
		/// </summary>
		[JsonProperty("processed_field_count")]
		long ProcessedFieldCount { get; }

		/// <summary>
		/// Total input bytes.
		/// </summary>
		[JsonProperty("input_bytes")]
		long InputBytes { get; }

		/// <summary>
		/// The count of input fields.
		/// </summary>
		[JsonProperty("input_field_count")]
		long InputFieldCount { get; }

		/// <summary>
		/// The count of invalid dates.
		/// </summary>
		[JsonProperty("invalid_date_count")]
		long InvalidDateCount { get; }

		/// <summary>
		/// The count of missing fields.
		/// </summary>
		[JsonProperty("missing_field_count")]
		long MissingFieldCount { get; }

		/// <summary>
		/// The count of out of order timestamps.
		/// </summary>
		[JsonProperty("out_of_order_timestamp_count")]
		long OutOfOrderTimestampCount { get; }

		/// <summary>
		/// The count of empty buckets.
		/// </summary>
		[JsonProperty("empty_bucket_count")]
		long EmptyBucketCount { get; }

		/// <summary>
		/// The count of sparse buckets.
		/// </summary>
		[JsonProperty("sparse_bucket_count")]
		long SparseBucketCount { get; }

		/// <summary>
		/// The count of buckets.
		/// </summary>
		[JsonProperty("bucket_count")]
		long BucketCount { get; }

		/// <summary>
		/// The time of the last data item.
		/// </summary>
		[JsonProperty("last_data_time")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset LastDataTime { get; }

		/// <summary>
		/// The count of input records.
		/// </summary>
		[JsonProperty("input_record_count")]
		long InputRecordCount { get; }

		/// <summary>
		/// The earliest record timestamp
		/// </summary>
		[JsonProperty("earliest_record_timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? EarliestRecordTimestamp { get; }

		/// <summary>
		/// The latest record timestamp
		/// </summary>
		[JsonProperty("latest_record_timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? LatestRecordTimestamp { get; }
	}

	/// <inheritdoc cref="IPostJobDataResponse" />
	public class PostJobDataResponse : ResponseBase, IPostJobDataResponse
	{
		/// <inheritdoc />
		public string JobId { get; internal set; }

		/// <inheritdoc />
		public long ProcessedRecordCount { get; internal set; }

		/// <inheritdoc />
		public long ProcessedFieldCount { get; internal set; }

		/// <inheritdoc />
		public long InputBytes { get; internal set; }

		/// <inheritdoc />
		public long InputFieldCount { get; internal set; }

		/// <inheritdoc />
		public long InvalidDateCount { get; internal set; }

		/// <inheritdoc />
		public long MissingFieldCount { get; internal set; }

		/// <inheritdoc />
		public long OutOfOrderTimestampCount { get; internal set; }

		/// <inheritdoc />
		public long EmptyBucketCount { get; internal set; }

		/// <inheritdoc />
		public long SparseBucketCount { get; internal set; }

		/// <inheritdoc />
		public long BucketCount { get; internal set; }

		/// <inheritdoc />
		public DateTimeOffset LastDataTime { get; internal set; }

		/// <inheritdoc />
		public long InputRecordCount { get; internal set; }

		/// <inheritdoc />
		public DateTimeOffset? EarliestRecordTimestamp { get; internal set;}

		/// <inheritdoc />
		public DateTimeOffset? LatestRecordTimestamp { get; internal set; }
	}
}
