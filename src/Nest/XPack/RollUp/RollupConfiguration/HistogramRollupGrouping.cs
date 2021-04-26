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
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary> The histogram group aggregates one or more numeric fields into numeric histogram intervals. </summary>
	[ReadAs(typeof(HistogramRollupGrouping))]
	public interface IHistogramRollupGrouping
	{
		/// <summary>
		/// The set of fields that you wish to build histograms for. All fields specified must be some kind of numeric. Order does not matter
		/// </summary>
		[DataMember(Name ="fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// The interval of histogram buckets to be generated when rolling up. Note that only one interval can be specified in the
		/// histogram group, meaning that all fields being grouped via the histogram must share the same interval.
		/// </summary>
		[DataMember(Name ="interval")]
		long? Interval { get; set; }
	}

	/// <inheritdoc />
	public class HistogramRollupGrouping : IHistogramRollupGrouping
	{
		/// <inheritdoc />
		public Fields Fields { get; set; }

		/// <inheritdoc />
		public long? Interval { get; set; }
	}

	/// <inheritdoc cref="IHistogramRollupGrouping" />
	public class HistogramRollupGroupingDescriptor<T>
		: DescriptorBase<HistogramRollupGroupingDescriptor<T>, IHistogramRollupGrouping>, IHistogramRollupGrouping
		where T : class
	{
		Fields IHistogramRollupGrouping.Fields { get; set; }
		long? IHistogramRollupGrouping.Interval { get; set; }

		/// <inheritdoc cref="IHistogramRollupGrouping.Fields" />
		public HistogramRollupGroupingDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="IHistogramRollupGrouping.Fields" />
		public HistogramRollupGroupingDescriptor<T> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		/// <inheritdoc cref="IHistogramRollupGrouping.Interval" />
		public HistogramRollupGroupingDescriptor<T> Interval(long? interval) => Assign(interval, (a, v) => a.Interval = v);
	}
}
