// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;


namespace Nest
{
	/// <summary>
	/// Retrieve job results for one or more buckets.
	/// </summary>
	[MapsApi("ml.get_buckets.json")]
	public partial interface IGetBucketsRequest
	{
		/// <summary>
		/// Returns buckets with anomaly scores higher than this value.
		/// </summary>
		[DataMember(Name ="anomaly_score")]
		double? AnomalyScore { get; set; }

		/// <summary>
		/// If true, the buckets are sorted in descending order.
		/// </summary>
		[DataMember(Name ="desc")]
		bool? Descending { get; set; }

		/// <summary>
		/// Returns buckets with timestamps earlier than this time.
		/// </summary>
		[DataMember(Name ="end")]
		DateTimeOffset? End { get; set; }

		/// <summary>
		/// If true, the output excludes interim results. By default, interim results are included.
		/// </summary>
		[DataMember(Name ="exclude_interim")]
		bool? ExcludeInterim { get; set; }

		/// <summary>
		/// If true, the output includes anomaly records.
		/// </summary>
		[DataMember(Name ="expand")]
		bool? Expand { get; set; }

		/// <summary>
		/// Specifies pagination for the buckets
		/// </summary>
		[DataMember(Name ="page")]
		IPage Page { get; set; }

		/// <summary>
		/// Specifies the sort field for the requested buckets. By default, the buckets are sorted by the timestamp field.
		/// </summary>
		[DataMember(Name ="sort")]
		Field Sort { get; set; }

		/// <summary>
		/// Returns buckets with timestamps after this time.
		/// </summary>
		[DataMember(Name ="start")]
		DateTimeOffset? Start { get; set; }
	}

	/// <inheritdoc />
	public partial class GetBucketsRequest
	{
		/// <inheritdoc />
		public double? AnomalyScore { get; set; }

		/// <inheritdoc />
		public bool? Descending { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }

		/// <inheritdoc />
		public bool? ExcludeInterim { get; set; }

		/// <inheritdoc />
		public bool? Expand { get; set; }

		/// <inheritdoc />
		public IPage Page { get; set; }

		/// <inheritdoc />
		public Field Sort { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }
	}

	/// <inheritdoc />
	public partial class GetBucketsDescriptor
	{
		double? IGetBucketsRequest.AnomalyScore { get; set; }
		bool? IGetBucketsRequest.Descending { get; set; }
		DateTimeOffset? IGetBucketsRequest.End { get; set; }
		bool? IGetBucketsRequest.ExcludeInterim { get; set; }
		bool? IGetBucketsRequest.Expand { get; set; }
		IPage IGetBucketsRequest.Page { get; set; }
		Field IGetBucketsRequest.Sort { get; set; }
		DateTimeOffset? IGetBucketsRequest.Start { get; set; }

		/// <inheritdoc />
		public GetBucketsDescriptor AnomalyScore(double? anomalyScore) => Assign(anomalyScore, (a, v) => a.AnomalyScore = v);

		/// <inheritdoc />
		public GetBucketsDescriptor Descending(bool? descending = true) => Assign(descending, (a, v) => a.Descending = v);

		/// <inheritdoc />
		public GetBucketsDescriptor End(DateTimeOffset? end) => Assign(end, (a, v) => a.End = v);

		/// <inheritdoc />
		public GetBucketsDescriptor ExcludeInterim(bool? excludeInterim = true) => Assign(excludeInterim, (a, v) => a.ExcludeInterim = v);

		/// <inheritdoc />
		public GetBucketsDescriptor Expand(bool? expand = true) => Assign(expand, (a, v) => a.Expand = v);

		/// <inheritdoc />
		public GetBucketsDescriptor Page(Func<PageDescriptor, IPage> selector) => Assign(selector, (a, v) => a.Page = v?.Invoke(new PageDescriptor()));

		/// <inheritdoc />
		public GetBucketsDescriptor Sort(Field field) => Assign(field, (a, v) => a.Sort = v);

		/// <inheritdoc />
		public GetBucketsDescriptor Start(DateTimeOffset? start) => Assign(start, (a, v) => a.Start = v);
	}
}
