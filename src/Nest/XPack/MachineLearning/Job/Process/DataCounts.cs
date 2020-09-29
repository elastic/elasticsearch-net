// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	public class DataCounts
	{
		/// <summary>
		/// The number of bucket results produced by the job.
		/// </summary>
		[DataMember(Name ="bucket_count")]
		public long BucketCount { get; internal set; }

		/// <summary>
		/// The timestamp of the earliest chronologically ordered record.
		/// </summary>
		[DataMember(Name ="earliest_record_timestamp")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? EarliestRecordTimestamp { get; internal set; }

		/// <summary>
		/// The number of buckets which did not contain any data. If your data contains many empty buckets,
		/// consider increasing your bucket_span or using functions that are tolerant to gaps in
		/// data such as mean, non_null_sum or non_zero_count.
		/// </summary>
		[DataMember(Name ="empty_bucket_count")]
		public long EmptyBucketCount { get; internal set; }

		/// <summary>
		/// The number of raw bytes read by the job.
		/// </summary>
		[DataMember(Name ="input_bytes")]
		public long InputBytes { get; internal set; }

		/// <summary>
		/// The total number of record fields read by the job.
		/// This count includes fields that are not used in the analysis.
		/// </summary>
		[DataMember(Name ="input_field_count")]
		public long InputFieldCount { get; internal set; }

		/// <summary>
		/// The number of data records read by the job.
		/// </summary>
		[DataMember(Name ="input_record_count")]
		public long InputRecordCount { get; internal set; }

		/// <summary>
		/// The number of records with either a missing date field or a date that could not be parsed.
		/// </summary>
		[DataMember(Name ="invalid_date_count")]
		public long InvalidDateCount { get; internal set; }

		/// <summary>
		///  A unique identifier for the job.
		/// </summary>
		[DataMember(Name ="job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The timestamp at which data was last analyzed, according to server time.
		/// </summary>
		[DataMember(Name ="last_data_time")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset LastDataTime { get; internal set; }

		/// <summary>
		/// The timestamp of the last bucket that did not contain any data.
		/// </summary>
		[DataMember(Name ="latest_empty_bucket_timestamp")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset LatestEmptyBucketTimestamp { get; internal set; }

		/// <summary>
		/// The timestamp of the last processed record.
		/// </summary>
		[DataMember(Name ="latest_record_timestamp")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset LatestRecordTimestamp { get; internal set; }

		/// <summary>
		/// The timestamp of the last bucket that was considered sparse.
		/// </summary>
		[DataMember(Name ="latest_sparse_bucket_timestamp")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset LatestSparseBucketTimestamp { get; internal set; }

		/// <summary>
		/// The number of records that are missing a field that the job is configured to analyze.
		/// Records with missing fields are still processed because it is possible that not all fields are missing.
		/// The value of <see cref="ProcessedRecordCount" /> includes this count.
		/// </summary>
		[DataMember(Name ="missing_field_count")]
		public long MissingFieldCount { get; internal set; }

		/// <summary>
		/// The number of records that are out of time sequence and outside of the latency window.
		/// These out of order records are discarded, since jobs require time series data to be in
		/// ascending chronological order.
		/// </summary>
		[DataMember(Name ="out_of_order_timestamp_count")]
		public long OutOfOrderTimestampCount { get; internal set; }

		/// <summary>
		/// The total number of fields in all the records that have been processed by the job.
		/// Only fields that are specified in the detector configuration contribute to this count.
		/// The time stamp is not included in this count.
		/// </summary>
		[DataMember(Name ="processed_field_count")]
		public long ProcessedFieldCount { get; internal set; }

		/// <summary>
		///  The number of records that have been processed by the job. This value includes records with missing fields,
		///  since they are nonetheless analyzed.
		///  If you use datafeeds and have aggregations in your search query, the <see cref="ProcessedRecordCount" />
		/// will be the number of aggregated records processed, not the number of Elasticsearch documents.
		/// </summary>
		[DataMember(Name ="processed_record_count")]
		public long ProcessedRecordCount { get; internal set; }

		[DataMember(Name ="sparse_bucket_count")]
		public long SparseBucketCount { get; internal set; }
	}
}
