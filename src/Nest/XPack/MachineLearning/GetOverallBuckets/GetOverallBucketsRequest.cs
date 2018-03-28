using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Retrieves overall bucket results that summarize the bucket results of multiple jobs.
	/// </summary>
	public partial interface IGetOverallBucketsRequest
	{
		/// <summary>
		/// If <c>false</c> and the job id does not match any job an error will be returned. The default value is true.
		/// </summary>
		[JsonProperty("allow_no_jobs")]
		bool? AllowNoJobs { get; set; }

		/// <summary>
		/// The span of the overall buckets.
		/// Must be greater or equal to the largest job’s bucket span. Defaults to the largest job’s bucket span.
		/// </summary>
		[JsonProperty("bucket_span")]
		Time BucketSpan { get; set; }

		/// <summary>
		/// Returns overall buckets with timestamps earlier than this time.
		/// </summary>
		[JsonProperty("end")]
		// Forced to prevent override, ML API always expects ISO8601 format
		[JsonConverter(typeof(IsoDateTimeConverter))]
		DateTimeOffset? End { get; set; }

		/// <summary>
		/// If true, the output excludes interim overall buckets. Overall buckets are interim if any of the job
		/// buckets within the overall bucket interval are interim. By default, interim results are included.
		/// </summary>
		[JsonProperty("exclude_interim")]
		bool? ExcludeInterim { get; set; }

		/// <summary>
		/// Returns overall buckets with overall scores greater or equal than this value.
		/// </summary>
		[JsonProperty("overall_score")]
		double? OverallScore { get; set; }

		/// <summary>
		/// Returns overall buckets with timestamps after this time.
		/// </summary>
		[JsonProperty("start")]
		// Forced to prevent override, ML API always expects ISO8601 format
		[JsonConverter(typeof(IsoDateTimeConverter))]
		DateTimeOffset? Start { get; set; }

		/// <summary>
		/// The number of top job bucket scores to be used in the
		/// <see cref="OverallBucket.OverallScore"/> calculation on the response.
		/// The default value is <c>1</c>.
		/// </summary>
		[JsonProperty("top_n")]
		int? TopN { get; set; }
	}

	/// <inheritdoc cref="IGetOverallBucketsRequest"/>
	public partial class GetOverallBucketsRequest : IGetOverallBucketsRequest
	{
		/// <inheritdoc />
		public bool? AllowNoJobs { get; set; }
		/// <inheritdoc />
		public Time BucketSpan { get; set; }
		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }
		/// <inheritdoc />
		public bool? ExcludeInterim { get; set; }
		/// <inheritdoc />
		public double? OverallScore { get; set; }
		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }
		/// <inheritdoc />
		public int? TopN { get; set; }
	}

	/// <inheritdoc cref="IGetOverallBucketsRequest" />
	[DescriptorFor("XpackMlGetOverallBuckets")]
	public partial class GetOverallBucketsDescriptor
	{
		bool? IGetOverallBucketsRequest.AllowNoJobs { get; set; }
		Time IGetOverallBucketsRequest.BucketSpan { get; set; }
		DateTimeOffset? IGetOverallBucketsRequest.End { get; set; }
		bool? IGetOverallBucketsRequest.ExcludeInterim { get; set; }
		double? IGetOverallBucketsRequest.OverallScore { get; set; }
		DateTimeOffset? IGetOverallBucketsRequest.Start { get; set; }
		int? IGetOverallBucketsRequest.TopN { get; set; }

		/// <inheritdoc cref="IGetOverallBucketsRequest.AllowNoJobs" />
		public GetOverallBucketsDescriptor AllowNoJobs(bool? allowNoJobs = true) =>
			Assign(a => a.AllowNoJobs = allowNoJobs);

		/// <inheritdoc cref="IGetOverallBucketsRequest.BucketSpan" />
		public GetOverallBucketsDescriptor BucketSpan(Time bucketSpan) =>
			Assign(a => a.BucketSpan = bucketSpan);

		/// <inheritdoc cref="IGetOverallBucketsRequest.End" />
		public GetOverallBucketsDescriptor End(DateTimeOffset? end) => Assign(a => a.End = end);

		/// <inheritdoc cref="IGetOverallBucketsRequest.ExcludeInterim" />
		public GetOverallBucketsDescriptor ExcludeInterim(bool? excludeInterim = true) =>
			Assign(a => a.ExcludeInterim = excludeInterim);

		/// <inheritdoc cref="IGetOverallBucketsRequest.OverallScore" />
		public GetOverallBucketsDescriptor OverallScore(double? overallScore) =>
			Assign(a => a.OverallScore = overallScore);

		/// <inheritdoc cref="IGetOverallBucketsRequest.Start" />
		public GetOverallBucketsDescriptor Start(DateTimeOffset? start) => Assign(a => a.Start = start);

		/// <inheritdoc cref="IGetOverallBucketsRequest.TopN" />
		public GetOverallBucketsDescriptor TopN(int? topN) => Assign(a => a.TopN = topN);
	}
}
