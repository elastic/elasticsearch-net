using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A request to retrieve retrieve anomaly records for a Machine Learning job.
	/// </summary>
	public partial interface IGetAnomalyRecordsRequest
	{
		/// <summary>
		/// If true, the results are sorted in descending order.
		/// </summary>
		[JsonProperty("desc")]
		bool? Descending { get; set; }

		/// <summary>
		/// If true, the output excludes interim results. By default, interim results are included.
		/// </summary>
		[JsonProperty("exclude_interim")]
		bool? ExcludeInterim { get; set; }

		/// <summary>
		/// Returns records with timestamps earlier than this time.
		/// </summary>
		[JsonProperty("end")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? End { get; set; }

		/// <summary>
		/// Specifies pagination for the records
		/// </summary>
		[JsonProperty("page")]
		IPage Page { get; set; }

		/// <summary>
		/// Returns records with anomaly scores higher than this value.
		/// </summary>
		[JsonProperty("record_score")]
		double? RecordScore { get; set; }

		/// <summary>
		/// Specifies the sort field for the requested records. By default, records are sorted by the record score value.
		/// </summary>
		[JsonProperty("sort")]
		Field Sort { get; set; }

		/// <summary>
		/// Returns records with timestamps after this time.
		/// </summary>
		[JsonProperty("start")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? Start { get; set; }
	}

	/// <inheritdoc />
	public partial class GetAnomalyRecordsRequest
	{
		/// <inheritdoc />
		public bool? Descending { get; set; }

		/// <inheritdoc />
		public bool? ExcludeInterim { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }

		/// <inheritdoc />
		public IPage Page { get; set; }

		/// <inheritdoc />
		public double? RecordScore { get; set; }

		/// <inheritdoc />
		public Field Sort { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlGetRecords")]
	public partial class GetAnomalyRecordsDescriptor
	{
		public GetAnomalyRecordsDescriptor() : base() { }

		bool? IGetAnomalyRecordsRequest.Descending { get; set; }
		DateTimeOffset? IGetAnomalyRecordsRequest.End { get; set; }
		IPage IGetAnomalyRecordsRequest.Page { get; set; }
		Field IGetAnomalyRecordsRequest.Sort { get; set; }
		DateTimeOffset? IGetAnomalyRecordsRequest.Start { get; set; }
		bool? IGetAnomalyRecordsRequest.ExcludeInterim { get; set; }
		double? IGetAnomalyRecordsRequest.RecordScore { get; set; }

		/// <inheritdoc />
		public GetAnomalyRecordsDescriptor Descending(bool descending = true) => Assign(a => a.Descending = descending);

		/// <inheritdoc />
		public GetAnomalyRecordsDescriptor End(DateTimeOffset end) => Assign(a => a.End = end);

		/// <inheritdoc />
		public GetAnomalyRecordsDescriptor Page(Func<PageDescriptor, IPage> selector) =>
			Assign(a => a.Page = selector?.Invoke(new PageDescriptor()));

		/// <inheritdoc />
		public GetAnomalyRecordsDescriptor Sort(Field field) => Assign(a => a.Sort = field);

		/// <inheritdoc />
		public GetAnomalyRecordsDescriptor Start(DateTimeOffset end) => Assign(a => a.Start = end);

		/// <inheritdoc />
		public GetAnomalyRecordsDescriptor ExcludeInterim(bool excludeInterim = true) =>
			Assign(a => a.ExcludeInterim = excludeInterim);

		/// <inheritdoc />
		public GetAnomalyRecordsDescriptor RecordScore(double recordScore) =>
			Assign(a => a.RecordScore = recordScore);
	}
}
