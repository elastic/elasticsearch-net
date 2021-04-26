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
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DateRangeAggregation))]
	public interface IDateRangeAggregation : IBucketAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="format")]
		string Format { get; set; }

		[DataMember(Name="missing")]
		object Missing { get; set; }

		[DataMember(Name ="ranges")]
		IEnumerable<IDateRangeExpression> Ranges { get; set; }

		[DataMember(Name ="time_zone")]
		string TimeZone { get; set; }
	}

	public class DateRangeAggregation : BucketAggregationBase, IDateRangeAggregation
	{
		internal DateRangeAggregation() { }

		public DateRangeAggregation(string name) : base(name) { }

		public Field Field { get; set; }
		public string Format { get; set; }
		public object Missing { get; set; }
		public IEnumerable<IDateRangeExpression> Ranges { get; set; }
		public string TimeZone { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.DateRange = this;
	}

	public class DateRangeAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<DateRangeAggregationDescriptor<T>, IDateRangeAggregation, T>
			, IDateRangeAggregation
		where T : class
	{
		Field IDateRangeAggregation.Field { get; set; }

		string IDateRangeAggregation.Format { get; set; }

		object IDateRangeAggregation.Missing { get; set; }

		IEnumerable<IDateRangeExpression> IDateRangeAggregation.Ranges { get; set; }

		string IDateRangeAggregation.TimeZone { get; set; }

		public DateRangeAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DateRangeAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public DateRangeAggregationDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		public DateRangeAggregationDescriptor<T> Missing(object missing) => Assign(missing, (a, v) => a.Missing = v);

		public DateRangeAggregationDescriptor<T> Ranges(params IDateRangeExpression[] ranges) =>
			Assign(ranges.ToListOrNullIfEmpty(), (a, v) => a.Ranges = v);

		public DateRangeAggregationDescriptor<T> TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		public DateRangeAggregationDescriptor<T> Ranges(params Func<DateRangeExpressionDescriptor, IDateRangeExpression>[] ranges) =>
			Assign(ranges?.Select(r => r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty(), (a, v) => a.Ranges = v);

		public DateRangeAggregationDescriptor<T> Ranges(IEnumerable<Func<DateRangeExpressionDescriptor, IDateRangeExpression>> ranges) =>
			Assign(ranges?.Select(r => r(new DateRangeExpressionDescriptor())).ToListOrNullIfEmpty(), (a, v) => a.Ranges = v);
	}
}
