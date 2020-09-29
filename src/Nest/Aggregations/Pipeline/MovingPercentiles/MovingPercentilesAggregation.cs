// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
