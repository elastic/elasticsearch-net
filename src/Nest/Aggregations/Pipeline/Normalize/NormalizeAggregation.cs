// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A parent pipeline aggregation which calculates the specific normalized/rescaled value for a specific bucket value.
	/// Values that cannot be normalized, will be skipped using the skip gap policy.
	/// <para />
	/// Valid in Elasticsearch 7.9.0+ with at least basic license level.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(NormalizeAggregation))]
	public interface INormalizeAggregation : IPipelineAggregation
	{
		[DataMember(Name = "method")]
		NormalizeMethod Method { get; set; }
	}

	/// <inheritdoc cref="INormalizeAggregation"/>
	public class NormalizeAggregation
		: PipelineAggregationBase, INormalizeAggregation
	{
		internal NormalizeAggregation() { }

		public NormalizeAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Normalize = this;

		/// <inheritdoc cref ="INormalizeAggregation.Method"/>
		public NormalizeMethod Method { get; set; }
	}

	/// <inheritdoc cref="INormalizeAggregation"/>
	public class NormalizeAggregationDescriptor
		: PipelineAggregationDescriptorBase<NormalizeAggregationDescriptor, INormalizeAggregation, SingleBucketsPath>
			, INormalizeAggregation
	{
		NormalizeMethod INormalizeAggregation.Method { get; set; }

		/// <inheritdoc cref ="INormalizeAggregation.Method"/>
		public NormalizeAggregationDescriptor Method(NormalizeMethod method) =>
			Assign(method, (a, v) => a.Method = v);
	}

	[StringEnum]
	public enum NormalizeMethod
	{
		/// <summary>
		/// rescales the data such that the minimum number is zero, and the maximum number is 1, with the rest normalized linearly in-between.
		/// </summary>
		[EnumMember(Value = "rescale_0_1")]
		RescaleZeroToOne,

		/// <summary>
		/// rescales the data such that the minimum number is zero, and the maximum number is 1, with the rest normalized linearly in-between.
		/// </summary>
		[EnumMember(Value = "rescale_0_100")]
		RescaleZeroToOneHundred,

		/// <summary>
		/// normalizes each value so that it represents a percentage of the total sum it attributes to.
		/// </summary>
		[EnumMember(Value = "percent_of_sum")]
		PercentOfSum,

		/// <summary>
		/// normalizes such that each value is normalized by how much it differs from the average.
		/// </summary>
		[EnumMember(Value = "mean")]
		Mean,

		/// <summary>
		/// normalizes such that each value represents how far it is from the mean relative to the standard deviation
		/// </summary>
		[EnumMember(Value = "zscore")]
		Zscore,

		/// <summary>
		/// normalizes such that each value is exponentiated and relative to the sum of the exponents of the original values.
		/// </summary>
		[EnumMember(Value = "softmax")]
		Softmax
	}
}
