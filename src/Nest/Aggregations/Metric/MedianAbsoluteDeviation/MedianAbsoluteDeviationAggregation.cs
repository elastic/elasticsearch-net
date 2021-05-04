// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// This single-value aggregation approximates the median absolute deviation of its search results.
	///
	///	Median absolute deviation is a measure of variability. It is a robust statistic, meaning that
	/// it is useful for describing data that may have outliers, or may not be normally distributed.
	/// For such data it can be more descriptive than standard deviation.
	///
	///	It is calculated as the median of each data point’s deviation from the median of the
	/// entire sample. That is, for a random variable <code>X</code>, the median absolute deviation
	/// is <code>median(|median(X) - Xi|).</code>
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(MedianAbsoluteDeviationAggregation))]
	public interface IMedianAbsoluteDeviationAggregation : IFormattableMetricAggregation
	{
		/// <summary>
		/// TDigest algorithm component that controls memory usage and approximation error.
		/// By increasing the compression value, you can increase the accuracy
		/// at the cost of more memory. Larger compression values also make
		/// the algorithm slower since the underlying tree data structure grows in
		/// size, resulting in more expensive operations. The default compression value is <c>100</c>.
		/// </summary>
		[DataMember(Name = "compression")]
		double? Compression { get; set; }
	}

	/// <inheritdoc cref="IMedianAbsoluteDeviationAggregation"/>
	public class MedianAbsoluteDeviationAggregation : FormattableMetricAggregationBase, IMedianAbsoluteDeviationAggregation
	{
		internal MedianAbsoluteDeviationAggregation() { }

		public MedianAbsoluteDeviationAggregation(string name, Field field) : base(name, field) { }

		/// <inheritdoc />
		public double? Compression { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.MedianAbsoluteDeviation = this;
	}

	/// <inheritdoc cref="IMedianAbsoluteDeviationAggregation"/>
	public class MedianAbsoluteDeviationAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<MedianAbsoluteDeviationAggregationDescriptor<T>, IMedianAbsoluteDeviationAggregation, T>
			, IMedianAbsoluteDeviationAggregation
		where T : class
	{
		double? IMedianAbsoluteDeviationAggregation.Compression { get; set; }

		/// <inheritdoc cref="IMedianAbsoluteDeviationAggregation.Compression"/>
		public MedianAbsoluteDeviationAggregationDescriptor<T> Compression(double? compression) => Assign(compression, (a, v) => a.Compression = v);
	}
}
