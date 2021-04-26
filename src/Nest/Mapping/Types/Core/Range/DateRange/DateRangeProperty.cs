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

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A range of date values represented as unsigned 64-bit integer milliseconds elapsed since system epoch.
	/// </summary>
	[InterfaceDataContract]
	public interface IDateRangeProperty : IRangeProperty
	{
		/// <summary>
		/// The date format(s) that can be parsed. Defaults to strict_date_optional_time||epoch_millis.
		/// <see cref="DateFormat" />
		/// </summary>
		[DataMember(Name ="format")]
		string Format { get; set; }
	}

	/// <inheritdoc cref="IDateRangeProperty" />
	public class DateRangeProperty : RangePropertyBase, IDateRangeProperty
	{
		public DateRangeProperty() : base(RangeType.DateRange) { }

		/// <inheritdoc />
		public string Format { get; set; }
	}

	/// <inheritdoc cref="IDateRangeProperty" />
	public class DateRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<DateRangePropertyDescriptor<T>, IDateRangeProperty, T>, IDateRangeProperty
		where T : class
	{
		public DateRangePropertyDescriptor() : base(RangeType.DateRange) { }

		string IDateRangeProperty.Format { get; set; }

		/// <inheritdoc />
		public DateRangePropertyDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);
	}
}
