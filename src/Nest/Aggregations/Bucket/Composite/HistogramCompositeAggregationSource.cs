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
	/// A value source that can be applied on numeric values to build fixed size interval over the values.
	/// The interval parameter defines how the numeric values should be transformed.
	/// For instance an interval set to 5 will translate any numeric values to its closest interval,
	/// a value of 101 would be translated to 100 which is the key for the interval between 100 and 105.
	/// </summary>
	public interface IHistogramCompositeAggregationSource : ICompositeAggregationSource
	{
		/// <summary>
		/// The interval to use when bucketing documents
		/// </summary>
		[DataMember(Name ="interval")]
		double? Interval { get; set; }

		/// <summary>
		/// A script to create the values for the composite buckets
		/// </summary>
		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc cref="IHistogramCompositeAggregationSource" />
	public class HistogramCompositeAggregationSource : CompositeAggregationSourceBase, IHistogramCompositeAggregationSource
	{
		public HistogramCompositeAggregationSource(string name) : base(name) { }

		/// <inheritdoc />
		public double? Interval { get; set; }

		/// <inheritdoc />
		public IScript Script { get; set; }

		/// <inheritdoc />
		protected override string SourceType => "histogram";
	}

	/// <inheritdoc cref="IHistogramCompositeAggregationSource" />
	public class HistogramCompositeAggregationSourceDescriptor<T>
		: CompositeAggregationSourceDescriptorBase<HistogramCompositeAggregationSourceDescriptor<T>, IHistogramCompositeAggregationSource, T>,
			IHistogramCompositeAggregationSource
	{
		public HistogramCompositeAggregationSourceDescriptor(string name) : base(name, "histogram") { }

		double? IHistogramCompositeAggregationSource.Interval { get; set; }
		IScript IHistogramCompositeAggregationSource.Script { get; set; }

		/// <inheritdoc cref="IHistogramCompositeAggregationSource.Interval" />
		public HistogramCompositeAggregationSourceDescriptor<T> Interval(double? interval) =>
			Assign(interval, (a, v) => a.Interval = v);

		/// <inheritdoc cref="IHistogramCompositeAggregationSource.Script" />
		public HistogramCompositeAggregationSourceDescriptor<T> Script(Func<ScriptDescriptor, IScript> selector) =>
			Assign(selector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
