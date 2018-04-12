using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class OverallBucket
	{
		/// <summary>
		/// The start time of the bucket. This timestamp uniquely identifies the bucket.
		/// </summary>
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		/// <summary>
		/// The length of the bucket. This value matches the bucket_span that is specified in the job.
		/// </summary>
		[JsonProperty("bucket_span")]
		public long BucketSpan { get; internal set; }

		/// <summary>
		/// The overall score
		/// </summary>
		[JsonProperty("overall_score")]
		public double OverallScore { get; internal set; }

		/// <summary>
		/// An array of partition score objects.
		/// </summary>
		[JsonProperty("jobs")]
		public IReadOnlyCollection<OverallBucketJobInfo> Jobs { get; internal set; } =
			EmptyReadOnly<OverallBucketJobInfo>.Collection;

		/// <summary>
		/// If true, this is an interim result. In other words, the bucket results are calculated based on partial input data.
		/// </summary>
		[JsonProperty("is_interim")]
		public bool IsInterim { get; internal set; }

		/// <summary>
		/// Internal. This value is always set to overall_bucket.
		/// </summary>
		[JsonProperty("result_type")]
		public string ResultType { get; internal set; }
	}

	public class OverallBucketJobInfo
	{
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		[JsonProperty("max_anomaly_score")]
		public double MaxAnomalyScore { get; internal set; }
	}
}
