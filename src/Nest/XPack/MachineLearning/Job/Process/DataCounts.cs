using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class DataCounts
	{
		/// <summary>
		///  A unique identifier for the job.
		/// </summary>
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The number of records that have been processed by the job. This value includes records with missing fields,
		/// since they are nonetheless analyzed.
		///
		/// If you use datafeeds and have aggregations in your search query, the <see cref="ProcessedRecordCount"/>
		/// will be the number of aggregated records processed, not the number of Elasticsearch documents.
		/// </summary>
		[JsonProperty("processed_record_count")]
		public long ProcessedRecordCount { get; internal set; }

		/// <summary>
		/// The total number of fields in all the records that have been processed by the job.
		/// Only fields that are specified in the detector configuration contribute to this count.
		/// The time stamp is not included in this count.
		/// </summary>
		[JsonProperty("processed_field_count")]
		public long ProcessedFieldCount { get; internal set; }

		/// <summary>
		/// The number of raw bytes read by the job.
		/// </summary>
		[JsonProperty("input_bytes")]
		public long InputBytes { get; internal set; }

		/// <summary>
		/// The total number of record fields read by the job.
		/// This count includes fields that are not used in the analysis.
		/// </summary>
		[JsonProperty("input_field_count")]
		public long InputFieldCount { get; internal set; }

		/// <summary>
		/// The number of records with either a missing date field or a date that could not be parsed.
		/// </summary>
		[JsonProperty("invalid_date_count")]
		public long InvalidDateCount { get; internal set; }

		/// <summary>
		/// The number of records that are missing a field that the job is configured to analyze.
		/// Records with missing fields are still processed because it is possible that not all fields are missing.
		/// The value of <see cref="ProcessedRecordCount"/> includes this count.
		/// </summary>
		[JsonProperty("missing_field_count")]
		public long MissingFieldCount { get; internal set; }

		/// <summary>
		/// The number of records that are out of time sequence and outside of the latency window.
		/// These out of order records are discarded, since jobs require time series data to be in
		/// ascending chronological order.
		/// </summary>
		[JsonProperty("out_of_order_timestamp_count")]
		public long OutOfOrderTimestampCount { get; internal set; }

		/// <summary>
		/// The number of buckets which did not contain any data. If your data contains many empty buckets,
		/// consider increasing your bucket_span or using functions that are tolerant to gaps in
		/// data such as mean, non_null_sum or non_zero_count.
		/// </summary>
		[JsonProperty("empty_bucket_count")]
		public long EmptyBucketCount { get; internal set; }

		[JsonProperty("sparse_bucket_count")]
		public long SparseBucketCount { get; internal set; }

		/// <summary>
		/// The number of bucket results produced by the job.
		/// </summary>
		[JsonProperty("bucket_count")]
		public long BucketCount { get; internal set; }

		/// <summary>
		/// The timestamp of the earliest chronologically ordered record.
		/// </summary>
		[JsonProperty("earliest_record_timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset? EarliestRecordTimestamp { get; internal set; }

		/// <summary>
		/// The timestamp of the last processed record.
		/// </summary>
		[JsonProperty("latest_record_timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset LatestRecordTimestamp { get; internal set; }

		/// <summary>
		/// The timestamp of the last bucket that did not contain any data.
		/// </summary>
		[JsonProperty("latest_empty_bucket_timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset LatestEmptyBucketTimestamp { get; internal set; }

		/// <summary>
		/// The timestamp at which data was last analyzed, according to server time.
		/// </summary>
		[JsonProperty("last_data_time")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset LastDataTime { get; internal set; }

		/// <summary>
		/// The timestamp of the last bucket that was considered sparse.
		/// </summary>
		[JsonProperty("latest_sparse_bucket_timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset LatestSparseBucketTimestamp { get; internal set; }

		/// <summary>
		/// The number of data records read by the job.
		/// </summary>
		[JsonProperty("input_record_count")]
		public long InputRecordCount { get; internal set; }
	}
}
