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
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// the Moving Percentile aggregation will slide a window across those percentiles and allow the user to compute the cumulative percentile.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(MovingPercentilesAggregation))]
	public interface IMovingPercentilesAggregation : IPipelineAggregation
	{
		/// <summary>
		/// The size of window to "slide" across the histogram.
		/// </summary>
		[DataMember(Name ="window")]
		int? Window { get; set; }

		/// <summary>
		/// Shift of window position.
		/// </summary>
		[DataMember(Name ="shift")]
		int? Shift { get; set; }
	}

	/// <inheritdoc cref="IMovingPercentilesAggregation"/>
	public class MovingPercentilesAggregation
		: PipelineAggregationBase, IMovingPercentilesAggregation
	{
		internal MovingPercentilesAggregation() { }

		public MovingPercentilesAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		/// <inheritdoc cref="IMovingPercentilesAggregation.Window"/>
		public int? Window { get; set; }

		/// <inheritdoc cref="IMovingPercentilesAggregation.Shift"/>
		public int? Shift { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.MovingPercentiles = this;
	}

	public class MovingPercentilesAggregationDescriptor
		: PipelineAggregationDescriptorBase<MovingPercentilesAggregationDescriptor, IMovingPercentilesAggregation, SingleBucketsPath>
			, IMovingPercentilesAggregation
	{
		int? IMovingPercentilesAggregation.Window { get; set; }
		int? IMovingPercentilesAggregation.Shift { get; set; }

		/// <inheritdoc cref="IMovingPercentilesAggregation.Window"/>
		public MovingPercentilesAggregationDescriptor Window(int? windowSize) => Assign(windowSize, (a, v) => a.Window = v);

		/// <inheritdoc cref="IMovingPercentilesAggregation.Shift"/>
		public MovingPercentilesAggregationDescriptor Shift(int? shift) => Assign(shift, (a, v) => a.Shift = v);
	}
}
