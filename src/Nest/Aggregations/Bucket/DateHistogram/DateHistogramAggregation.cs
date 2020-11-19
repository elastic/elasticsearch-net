// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DateHistogramAggregation))]
	public interface IDateHistogramAggregation : IBucketAggregation
	{
		/// <summary>
		/// Extend the bounds of the date histogram beyond the data itself,
		/// forcing the aggregation to start building buckets on
		/// a specific min and/or max value.
		/// Using extended bounds only makes sense when <see cref="MinimumDocumentCount"/> is 0
		/// as empty buckets will never be returned if it is greater than 0.
		/// </summary>
		[DataMember(Name ="extended_bounds")]
		ExtendedBounds<DateMath> ExtendedBounds { get; set; }

		/// <summary>
		/// The hard_bounds is a counterpart of extended_bounds and can limit the range of buckets in the histogram.
		/// It is particularly useful in the case of open data ranges that can result in a very large number of buckets.
		/// </summary>
		[DataMember(Name = "hard_bounds")]
		HardBounds<DateMath> HardBounds { get; set; }

		/// <summary>
		/// The field to target
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// Return a formatted date string as the key instead an epoch long
		/// </summary>
		[DataMember(Name ="format")]
		string Format { get; set; }

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		[DataMember(Name ="interval")]
		Union<DateInterval, Time> Interval { get; set; }

		/// <summary>
		/// The calendar interval to use when bucketing documents
		/// </summary>
		[DataMember(Name ="calendar_interval")]
		Union<DateInterval?, DateMathTime> CalendarInterval { get; set; }

		/// <summary>
		/// The fixed interval to use when bucketing documents
		/// </summary>
		[DataMember(Name ="fixed_interval")]
		Time FixedInterval { get; set; }

		/// <summary>
		/// The minimum number of documents that a bucket must contain to be returned in the response.
		/// The default is 0 meaning that buckets with no documents will be returned.
		/// </summary>
		[DataMember(Name ="min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		/// <summary>
		/// Defines how to treat documents that are missing a value. By default, they are ignored,
		/// but it is also possible to treat them as if they have a value.
		/// </summary>
		[DataMember(Name ="missing")]
		DateTime? Missing { get; set; }

		/// <summary>
		/// Change the start value of each bucket by the specified positive (+) or negative offset (-) duration,
		/// such as 1h for an hour, or 1d for a day.
		/// </summary>
		[DataMember(Name ="offset")]
		string Offset { get; set; }

		/// <summary>
		/// Defines an order in which returned buckets are sorted.
		/// By default the returned buckets are sorted by their key ascending.
		/// </summary>
		[DataMember(Name ="order")]
		HistogramOrder Order { get; set; }

		/// <inheritdoc cref="IScript"/>
		[DataMember(Name ="script")]
		IScript Script { get; set; }

		/// <summary>
		/// Used to indicate that bucketing should use a different time zone.
		/// Time zones may either be specified as an ISO 8601 UTC offset (e.g. +01:00 or -08:00)
		/// or as a timezone id, an identifier used in the TZ database like America/Los_Angeles.
		/// </summary>
		[DataMember(Name ="time_zone")]
		string TimeZone { get; set; }
	}

	public class DateHistogramAggregation : BucketAggregationBase, IDateHistogramAggregation
	{
		private string _format;

		internal DateHistogramAggregation() { }

		public DateHistogramAggregation(string name) : base(name) { }

		/// <inheritdoc />
		public ExtendedBounds<DateMath> ExtendedBounds { get; set; }
		/// <inheritdoc />
		public HardBounds<DateMath> HardBounds { get; set; }
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public string Format
		{
			get => !string.IsNullOrEmpty(_format) &&
				!_format.Contains("date_optional_time") &&
				(ExtendedBounds != null || HardBounds != null || Missing.HasValue)
					? _format + "||date_optional_time"
					: _format;
			set => _format = value;
		}

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		public Union<DateInterval, Time> Interval { get; set; }
		/// <inheritdoc />
		public Union<DateInterval?, DateMathTime> CalendarInterval { get; set; }
		/// <inheritdoc />
		public Time FixedInterval { get; set; }
		/// <inheritdoc />
		public int? MinimumDocumentCount { get; set; }
		/// <inheritdoc />
		public DateTime? Missing { get; set; }
		/// <inheritdoc />
		public string Offset { get; set; }
		/// <inheritdoc />
		public HistogramOrder Order { get; set; }
		/// <inheritdoc />
		public IScript Script { get; set; }
		/// <inheritdoc />
		public string TimeZone { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.DateHistogram = this;
	}

	public class DateHistogramAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<DateHistogramAggregationDescriptor<T>, IDateHistogramAggregation, T>
			, IDateHistogramAggregation
		where T : class
	{
		private string _format;

		ExtendedBounds<DateMath> IDateHistogramAggregation.ExtendedBounds { get; set; }
		HardBounds<DateMath> IDateHistogramAggregation.HardBounds { get; set; }
		Field IDateHistogramAggregation.Field { get; set; }

		//see: https://github.com/elastic/elasticsearch/issues/9725
		string IDateHistogramAggregation.Format
		{
			get => !string.IsNullOrEmpty(_format) &&
				!_format.Contains("date_optional_time") &&
				(Self.ExtendedBounds != null || Self.HardBounds != null || Self.Missing.HasValue)
					? _format + "||date_optional_time"
					: _format;
			set => _format = value;
		}

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		Union<DateInterval, Time> IDateHistogramAggregation.Interval { get; set; }
		Union<DateInterval?, DateMathTime> IDateHistogramAggregation.CalendarInterval { get; set; }
		Time IDateHistogramAggregation.FixedInterval { get; set; }
		int? IDateHistogramAggregation.MinimumDocumentCount { get; set; }
		DateTime? IDateHistogramAggregation.Missing { get; set; }
		string IDateHistogramAggregation.Offset { get; set; }
		HistogramOrder IDateHistogramAggregation.Order { get; set; }
		IScript IDateHistogramAggregation.Script { get; set; }
		string IDateHistogramAggregation.TimeZone { get; set; }

		/// <inheritdoc cref="IDateHistogramAggregation.Field" />
		public DateHistogramAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Field" />
		public DateHistogramAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Script" />
		public DateHistogramAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Script" />
		public DateHistogramAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		public DateHistogramAggregationDescriptor<T> Interval(Time interval) => Assign(interval, (a, v) => a.Interval = v);

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		public DateHistogramAggregationDescriptor<T> Interval(DateInterval interval) =>
			Assign(interval, (a, v) => a.Interval = v);

		/// <inheritdoc cref="IDateHistogramAggregation.CalendarInterval" />
		public DateHistogramAggregationDescriptor<T> CalendarInterval(DateMathTime interval) => Assign(interval, (a, v) => a.CalendarInterval = v);

		/// <inheritdoc cref="IDateHistogramAggregation.CalendarInterval" />
		public DateHistogramAggregationDescriptor<T> CalendarInterval(DateInterval? interval) => Assign(interval, (a, v) => a.CalendarInterval = v);

		/// <inheritdoc cref="IDateHistogramAggregation.FixedInterval" />
		public DateHistogramAggregationDescriptor<T> FixedInterval(Time interval) => Assign(interval, (a, v) => a.FixedInterval = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Format" />
		public DateHistogramAggregationDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		/// <inheritdoc cref="IDateHistogramAggregation.MinimumDocumentCount" />
		public DateHistogramAggregationDescriptor<T> MinimumDocumentCount(int? minimumDocumentCount) =>
			Assign(minimumDocumentCount, (a, v) => a.MinimumDocumentCount = v);

		/// <inheritdoc cref="IDateHistogramAggregation.TimeZone" />
		public DateHistogramAggregationDescriptor<T> TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Offset" />
		public DateHistogramAggregationDescriptor<T> Offset(string offset) => Assign(offset, (a, v) => a.Offset = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Order" />
		public DateHistogramAggregationDescriptor<T> Order(HistogramOrder order) => Assign(order, (a, v) => a.Order = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Order" />
		public DateHistogramAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(new HistogramOrder { Key = key, Order = SortOrder.Descending }, (a, v) => a.Order = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Order" />
		public DateHistogramAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(new HistogramOrder { Key = key, Order = SortOrder.Descending }, (a, v) => a.Order = v);

		/// <inheritdoc cref="IDateHistogramAggregation.ExtendedBounds" />
		public DateHistogramAggregationDescriptor<T> ExtendedBounds(DateMath min, DateMath max) =>
			Assign(new ExtendedBounds<DateMath> { Minimum = min, Maximum = max }, (a, v) => a.ExtendedBounds = v);

		/// <inheritdoc cref="IDateHistogramAggregation.HardBounds" />
		public DateHistogramAggregationDescriptor<T> HardBounds(DateMath min, DateMath max) =>
			Assign(new HardBounds<DateMath> { Minimum = min, Maximum = max }, (a, v) => a.HardBounds = v);

		/// <inheritdoc cref="IDateHistogramAggregation.Missing" />
		public DateHistogramAggregationDescriptor<T> Missing(DateTime? missing) => Assign(missing, (a, v) => a.Missing = v);
	}
}
