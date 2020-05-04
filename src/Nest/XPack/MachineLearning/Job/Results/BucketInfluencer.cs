// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class BucketInfluencer
	{
		/// <summary>
		/// The length of the bucket. This value matches the bucket_span that is specified in the job.
		/// </summary>
		[DataMember(Name ="bucket_span")]
		public long BucketSpan { get; internal set; }

		/// <summary>
		/// The field name of the influencer.
		/// </summary>
		[DataMember(Name ="influencer_field_name")]
		public string InfluencerFieldName { get; internal set; }

		/// <summary>
		/// The entity that influenced, contributed to, or was to blame for the anomaly.
		/// </summary>
		[DataMember(Name ="influencer_field_value")]
		public string InfluencerFieldValue { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the influencer in this bucket aggregated across detectors. Updated
		/// by a re-normalization process as new data is analyzed.
		/// </summary>
		[DataMember(Name ="influencer_score")]
		public double InfluencerScore { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the influencer in this bucket aggregated across detectors.
		/// </summary>
		[DataMember(Name ="initial_influencer_score")]
		public double InitialInfluencerScore { get; internal set; }

		/// <summary>
		/// If true, this is an interim result. In other words, the influencer results are calculated based on partial input data.
		/// </summary>
		[DataMember(Name ="is_interim")]
		public bool IsInterim { get; internal set; }

		// <summary>
		/// The unique identifier for the job that these results belong to.
		/// </summary>
		[DataMember(Name ="job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The probability that the influencer has this behavior, in the range 0 to 1. This value can be held to a high precision of over 300 decimal
		/// places.
		/// </summary>
		[DataMember(Name ="probability")]
		public double Probability { get; internal set; }

		/// <summary>
		/// Internal. This value is always set to influencer.
		/// </summary>
		[DataMember(Name ="result_type")]
		public string ResultType { get; internal set; }

		/// <summary>
		/// The start time of the bucket for which these results were calculated.
		/// </summary>
		[DataMember(Name ="timestamp")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset Timestamp { get; internal set; }
	}
}
