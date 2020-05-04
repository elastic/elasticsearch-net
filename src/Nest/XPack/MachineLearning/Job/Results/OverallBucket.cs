// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public class OverallBucket
	{
		/// <summary>
		/// The length of the bucket. This value matches the bucket_span that is specified in the job.
		/// </summary>
		[DataMember(Name = "bucket_span")]
		public long BucketSpan { get; internal set; }

		/// <summary>
		/// If true, this is an interim result. In other words, the bucket results are calculated based on partial input data.
		/// </summary>
		[DataMember(Name = "is_interim")]
		public bool IsInterim { get; internal set; }

		/// <summary>
		/// An array of partition score objects.
		/// </summary>
		[DataMember(Name = "jobs")]
		public IReadOnlyCollection<OverallBucketJobInfo> Jobs { get; internal set; } =
			EmptyReadOnly<OverallBucketJobInfo>.Collection;

		/// <summary>
		/// The overall score
		/// </summary>
		[DataMember(Name = "overall_score")]
		public double OverallScore { get; internal set; }

		/// <summary>
		/// Internal. This value is always set to overall_bucket.
		/// </summary>
		[DataMember(Name = "result_type")]
		public string ResultType { get; internal set; }

		/// <summary>
		/// The start time of the bucket. This timestamp uniquely identifies the bucket.
		/// </summary>
		[DataMember(Name = "timestamp")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset Timestamp { get; internal set; }
	}

	public class OverallBucketJobInfo
	{
		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		[DataMember(Name = "max_anomaly_score")]
		public double MaxAnomalyScore { get; internal set; }
	}
}
