// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A date_histogram group aggregates a date field into time-based buckets. The date_histogram group is mandatory ,
	/// you currently cannot rollup documents without a timestamp and a date_histogram group
	/// </summary>
	[ReadAs(typeof(DateHistogramRollupGrouping))]
	public interface IDateHistogramRollupGrouping
	{
		/// <summary>
		/// How long to wait before rolling up new documents. By default, the indexer attempts to roll up all data that is
		/// available.
		/// </summary>
		[DataMember(Name = "delay")]
		Time Delay { get; set; }

		/// <summary>
		/// The date field that is to be rolled up
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		/// <summary>
		/// Date format. Supports expressive date format pattern.
		/// </summary>
		[DataMember(Name = "format")]
		string Format { get; set; }

		/// <summary>
		/// The interval of time buckets to be generated when rolling up. E.g. "60m" will produce 60 minute (hourly) rollups.
		/// The interval defines the minimum interval that can be aggregated only.
		/// </summary>
		[DataMember(Name = "interval")]
		Time Interval { get; set; }

		/// <summary>
		/// Defines what time_zone the rollup documents are stored as. Unlike raw data, which can shift timezones on the fly,
		/// rolled
		/// documents have to be stored with a specific timezone. By default, rollup documents are stored in UT
		/// </summary>
		[DataMember(Name = "time_zone")]
		string TimeZone { get; set; }
	}

	/// <inheritdoc />
	public class DateHistogramRollupGrouping : IDateHistogramRollupGrouping
	{
		/// <inheritdoc />
		public Time Delay { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public string Format { get; set; }

		/// <inheritdoc />
		public Time Interval { get; set; }

		/// <inheritdoc />
		public string TimeZone { get; set; }
	}

	/// <inheritdoc cref="IDateHistogramRollupGrouping" />
	public class DateHistogramRollupGroupingDescriptor<T>
		: DescriptorBase<DateHistogramRollupGroupingDescriptor<T>, IDateHistogramRollupGrouping>, IDateHistogramRollupGrouping
		where T : class
	{
		Time IDateHistogramRollupGrouping.Delay { get; set; }
		Field IDateHistogramRollupGrouping.Field { get; set; }

		string IDateHistogramRollupGrouping.Format { get; set; }
		Time IDateHistogramRollupGrouping.Interval { get; set; }
		string IDateHistogramRollupGrouping.TimeZone { get; set; }

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Field" />
		public DateHistogramRollupGroupingDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Field" />
		public DateHistogramRollupGroupingDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Interval" />
		public DateHistogramRollupGroupingDescriptor<T> Interval(Time interval) => Assign(interval, (a, v) => a.Interval = v);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Delay" />
		public DateHistogramRollupGroupingDescriptor<T> Delay(Time delay) => Assign(delay, (a, v) => a.Delay = v);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.TimeZone" />
		public DateHistogramRollupGroupingDescriptor<T> TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Format" />
		public DateHistogramRollupGroupingDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);
	}
}
