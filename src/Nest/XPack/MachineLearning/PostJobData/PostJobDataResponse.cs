// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The Post Job Data API response
	/// </summary>
	public class PostJobDataResponse : ResponseBase
	{
		/// <summary>The count of buckets. </summary>
		[DataMember(Name = "bucket_count")]
		public long BucketCount { get; internal set; }

		/// <summary> The earliest record timestamp</summary>
		[DataMember(Name = "earliest_record_timestamp")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? EarliestRecordTimestamp { get; internal set; }

		/// <summaryThe count of empty buckets.</summary>
		[DataMember(Name = "empty_bucket_count")]
		public long EmptyBucketCount { get; internal set; }

		/// <summary>Total input bytes.</summary>
		[DataMember(Name = "input_bytes")]
		public long InputBytes { get; internal set; }

		/// <summary> The count of input fields. </summary>
		[DataMember(Name = "input_field_count")]
		public long InputFieldCount { get; internal set; }

		/// <summary> The count of input records. </summary>
		[DataMember(Name = "input_record_count")]
		public long InputRecordCount { get; internal set; }

		/// <summary> The count of invalid dates. </summary>
		[DataMember(Name = "invalid_date_count")]
		public long InvalidDateCount { get; internal set; }

		/// <summary>The unique identifier for the job.</summary>
		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		/// <summary> The time of the last data item. </summary>
		[DataMember(Name = "last_data_time")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset LastDataTime { get; internal set; }

		/// <summary> The latest record timestamp </summary>
		[DataMember(Name = "latest_record_timestamp")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? LatestRecordTimestamp { get; internal set; }

		/// <summary> The count of missing fields. </summary>
		[DataMember(Name = "missing_field_count")]
		public long MissingFieldCount { get; internal set; }

		/// <summary>
		/// The count of out of order timestamps.
		/// </summary>
		[DataMember(Name = "out_of_order_timestamp_count")]
		public long OutOfOrderTimestampCount { get; internal set; }

		/// <summary> The count of processed fields. </summary>
		[DataMember(Name = "processed_field_count")]
		public long ProcessedFieldCount { get; internal set; }

		/// <summary> The count of processed records. </summary>
		[DataMember(Name = "processed_record_count")]
		public long ProcessedRecordCount { get; internal set; }

		/// <summary> The count of sparse buckets. </summary>
		[DataMember(Name = "sparse_bucket_count")]
		public long SparseBucketCount { get; internal set; }
	}
}
