/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
