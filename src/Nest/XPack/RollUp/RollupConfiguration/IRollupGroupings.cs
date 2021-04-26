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
	/// <summary>
	/// The groups section of the configuration is where you decide which fields should be grouped on, and with what aggregations.
	/// These fields will then be available later for aggregating into buckets
	/// </summary>
	[ReadAs(typeof(RollupGroupings))]
	public interface IRollupGroupings
	{
		/// <inheritdoc cref="IDateHistogramRollupGrouping" />
		[DataMember(Name ="date_histogram")]
		IDateHistogramRollupGrouping DateHistogram { get; set; }

		/// <inheritdoc cref="IHistogramRollupGrouping" />
		[DataMember(Name ="histogram")]
		IHistogramRollupGrouping Histogram { get; set; }

		/// <inheritdoc cref="ITermsRollupGrouping" />
		[DataMember(Name ="terms")]
		ITermsRollupGrouping Terms { get; set; }
	}

	/// <inheritdoc cref="IRollupGroupings" />
	public class RollupGroupings : IRollupGroupings
	{
		/// <inheritdoc />
		public IDateHistogramRollupGrouping DateHistogram { get; set; }

		/// <inheritdoc />
		public IHistogramRollupGrouping Histogram { get; set; }

		/// <inheritdoc />
		public ITermsRollupGrouping Terms { get; set; }
	}

	/// <inheritdoc cref="IRollupGroupings" />
	public class RollupGroupingsDescriptor<T> : DescriptorBase<RollupGroupingsDescriptor<T>, IRollupGroupings>, IRollupGroupings
		where T : class
	{
		IDateHistogramRollupGrouping IRollupGroupings.DateHistogram { get; set; }
		IHistogramRollupGrouping IRollupGroupings.Histogram { get; set; }
		ITermsRollupGrouping IRollupGroupings.Terms { get; set; }

		/// <inheritdoc cref="IDateHistogramRollupGrouping" />
		public RollupGroupingsDescriptor<T> DateHistogram(
			Func<DateHistogramRollupGroupingDescriptor<T>, IDateHistogramRollupGrouping> selector
		) =>
			Assign(selector, (a, v) => a.DateHistogram = v?.Invoke(new DateHistogramRollupGroupingDescriptor<T>()));

		/// <inheritdoc cref="IHistogramRollupGrouping" />
		public RollupGroupingsDescriptor<T> Histogram(Func<HistogramRollupGroupingDescriptor<T>, IHistogramRollupGrouping> selector) =>
			Assign(selector, (a, v) => a.Histogram = v?.Invoke(new HistogramRollupGroupingDescriptor<T>()));

		/// <inheritdoc cref="ITermsRollupGrouping" />
		public RollupGroupingsDescriptor<T> Terms(Func<TermsRollupGroupingDescriptor<T>, ITermsRollupGrouping> selector) =>
			Assign(selector, (a, v) => a.Terms = v?.Invoke(new TermsRollupGroupingDescriptor<T>()));
	}
}
