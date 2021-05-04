// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;


namespace Nest
{
	/// <summary>
	/// Retrieves overall bucket results that summarize the bucket results of multiple jobs.
	/// </summary>
	[MapsApi("ml.get_overall_buckets.json")]
	public partial interface IGetOverallBucketsRequest
	{
		/// <summary>
		/// If <c>false</c> and the job id does not match any job an error will be returned. The default value is true.
		/// </summary>
		[DataMember(Name ="allow_no_jobs")]
		bool? AllowNoJobs { get; set; }

		/// <summary>
		/// The span of the overall buckets.
		/// Must be greater or equal to the largest job’s bucket span. Defaults to the largest job’s bucket span.
		/// </summary>
		[DataMember(Name ="bucket_span")]
		Time BucketSpan { get; set; }

		/// <summary>
		/// Returns overall buckets with timestamps earlier than this time.
		/// </summary>
		[DataMember(Name ="end")]
		// Forced to prevent override, ML API always expects ISO8601 format
		DateTimeOffset? End { get; set; }

		/// <summary>
		/// If true, the output excludes interim overall buckets. Overall buckets are interim if any of the job
		/// buckets within the overall bucket interval are interim. By default, interim results are included.
		/// </summary>
		[DataMember(Name ="exclude_interim")]
		bool? ExcludeInterim { get; set; }

		/// <summary>
		/// Returns overall buckets with overall scores greater or equal than this value.
		/// </summary>
		[DataMember(Name ="overall_score")]
		double? OverallScore { get; set; }

		/// <summary>
		/// Returns overall buckets with timestamps after this time.
		/// </summary>
		[DataMember(Name ="start")]
		// Forced to prevent override, ML API always expects ISO8601 format
		DateTimeOffset? Start { get; set; }

		/// <summary>
		/// The number of top job bucket scores to be used in the
		/// <see cref="OverallBucket.OverallScore" /> calculation on the response.
		/// The default value is <c>1</c>.
		/// </summary>
		[DataMember(Name ="top_n")]
		int? TopN { get; set; }
	}

	/// <inheritdoc cref="IGetOverallBucketsRequest" />
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
			Assign(allowNoJobs, (a, v) => a.AllowNoJobs = v);

		/// <inheritdoc cref="IGetOverallBucketsRequest.BucketSpan" />
		public GetOverallBucketsDescriptor BucketSpan(Time bucketSpan) =>
			Assign(bucketSpan, (a, v) => a.BucketSpan = v);

		/// <inheritdoc cref="IGetOverallBucketsRequest.End" />
		public GetOverallBucketsDescriptor End(DateTimeOffset? end) => Assign(end, (a, v) => a.End = v);

		/// <inheritdoc cref="IGetOverallBucketsRequest.ExcludeInterim" />
		public GetOverallBucketsDescriptor ExcludeInterim(bool? excludeInterim = true) =>
			Assign(excludeInterim, (a, v) => a.ExcludeInterim = v);

		/// <inheritdoc cref="IGetOverallBucketsRequest.OverallScore" />
		public GetOverallBucketsDescriptor OverallScore(double? overallScore) =>
			Assign(overallScore, (a, v) => a.OverallScore = v);

		/// <inheritdoc cref="IGetOverallBucketsRequest.Start" />
		public GetOverallBucketsDescriptor Start(DateTimeOffset? start) => Assign(start, (a, v) => a.Start = v);

		/// <inheritdoc cref="IGetOverallBucketsRequest.TopN" />
		public GetOverallBucketsDescriptor TopN(int? topN) => Assign(topN, (a, v) => a.TopN = v);
	}
}
