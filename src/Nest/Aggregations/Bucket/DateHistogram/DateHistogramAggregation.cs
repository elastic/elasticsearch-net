// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DateHistogramAggregation))]
	public interface IDateHistogramAggregation : IBucketAggregation
	{
		[DataMember(Name ="extended_bounds")]
		ExtendedBounds<DateMath> ExtendedBounds { get; set; }

		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="format")]
		string Format { get; set; }

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		[DataMember(Name ="interval")]
		Union<DateInterval, Time> Interval { get; set; }

		[DataMember(Name ="calendar_interval")]
		Union<DateInterval, Time> CalendarInterval { get; set; }

		[DataMember(Name ="fixed_interval")]
		Union<DateInterval, Time> FixedInterval { get; set; }

		[DataMember(Name ="min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[DataMember(Name ="missing")]
		DateTime? Missing { get; set; }

		[DataMember(Name ="offset")]
		string Offset { get; set; }

		[DataMember(Name ="order")]
		HistogramOrder Order { get; set; }

		[DataMember(Name ="params")]
		IDictionary<string, object> Params { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }

		[DataMember(Name ="time_zone")]
		string TimeZone { get; set; }
	}

	public class DateHistogramAggregation : BucketAggregationBase, IDateHistogramAggregation
	{
		private string _format;

		internal DateHistogramAggregation() { }

		public DateHistogramAggregation(string name) : base(name) { }

		public ExtendedBounds<DateMath> ExtendedBounds { get; set; }
		public Field Field { get; set; }

		public string Format
		{
			get => !string.IsNullOrEmpty(_format) &&
				!_format.Contains("date_optional_time") &&
				(ExtendedBounds != null || Missing.HasValue)
					? _format + "||date_optional_time"
					: _format;
			set => _format = value;
		}


		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		public Union<DateInterval, Time> Interval { get; set; }
		public Union<DateInterval, Time> CalendarInterval { get; set; }
		public Union<DateInterval, Time> FixedInterval { get; set; }

		public int? MinimumDocumentCount { get; set; }
		public DateTime? Missing { get; set; }
		public string Offset { get; set; }
		public HistogramOrder Order { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public IScript Script { get; set; }
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
		Field IDateHistogramAggregation.Field { get; set; }

		//see: https://github.com/elastic/elasticsearch/issues/9725
		string IDateHistogramAggregation.Format
		{
			get => !string.IsNullOrEmpty(_format) &&
				!_format.Contains("date_optional_time") &&
				(Self.ExtendedBounds != null || Self.Missing.HasValue)
					? _format + "||date_optional_time"
					: _format;
			set => _format = value;
		}

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		Union<DateInterval, Time> IDateHistogramAggregation.Interval { get; set; }
		Union<DateInterval, Time> IDateHistogramAggregation.CalendarInterval { get; set; }
		Union<DateInterval, Time> IDateHistogramAggregation.FixedInterval { get; set; }

		int? IDateHistogramAggregation.MinimumDocumentCount { get; set; }

		DateTime? IDateHistogramAggregation.Missing { get; set; }

		string IDateHistogramAggregation.Offset { get; set; }

		HistogramOrder IDateHistogramAggregation.Order { get; set; }

		IDictionary<string, object> IDateHistogramAggregation.Params { get; set; }

		IScript IDateHistogramAggregation.Script { get; set; }

		string IDateHistogramAggregation.TimeZone { get; set; }

		public DateHistogramAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DateHistogramAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public DateHistogramAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public DateHistogramAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		public DateHistogramAggregationDescriptor<T> Interval(Time interval) => Assign(interval, (a, v) => a.Interval = v);

		[Obsolete("Deprecated in version 7.2.0, use CalendarInterval or FixedInterval instead")]
		public DateHistogramAggregationDescriptor<T> Interval(DateInterval interval) =>
			Assign(interval, (a, v) => a.Interval = v);

		public DateHistogramAggregationDescriptor<T> CalendarInterval(Time interval) => Assign(interval, (a, v) => a.CalendarInterval = v);
		public DateHistogramAggregationDescriptor<T> CalendarInterval(DateInterval interval) => Assign(interval, (a, v) => a.CalendarInterval = v);
		public DateHistogramAggregationDescriptor<T> FixedInterval(Time interval) => Assign(interval, (a, v) => a.FixedInterval = v);
		public DateHistogramAggregationDescriptor<T> FixedInterval(DateInterval interval) => Assign(interval, (a, v) => a.FixedInterval = v);

		public DateHistogramAggregationDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		public DateHistogramAggregationDescriptor<T> MinimumDocumentCount(int? minimumDocumentCount) =>
			Assign(minimumDocumentCount, (a, v) => a.MinimumDocumentCount = v);

		public DateHistogramAggregationDescriptor<T> TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		public DateHistogramAggregationDescriptor<T> Offset(string offset) => Assign(offset, (a, v) => a.Offset = v);

		public DateHistogramAggregationDescriptor<T> Order(HistogramOrder order) => Assign(order, (a, v) => a.Order = v);

		public DateHistogramAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(new HistogramOrder { Key = key, Order = SortOrder.Descending }, (a, v) => a.Order = v);

		public DateHistogramAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(new HistogramOrder { Key = key, Order = SortOrder.Descending }, (a, v) => a.Order = v);

		public DateHistogramAggregationDescriptor<T> ExtendedBounds(DateMath min, DateMath max) =>
			Assign(new ExtendedBounds<DateMath> { Minimum = min, Maximum = max }, (a, v) => a.ExtendedBounds = v);

		public DateHistogramAggregationDescriptor<T> Missing(DateTime? missing) => Assign(missing, (a, v) => a.Missing = v);
	}
}
