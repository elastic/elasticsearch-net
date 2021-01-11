// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(RateAggregation))]
	public interface IRateAggregation : IMetricAggregation
	{
		/// <summary>
		/// The <see cref="DateInterval"/> to use as the rate unit.
		/// </summary>
		[DataMember(Name = "unit")]
		DateInterval? Unit { get; set; }

		/// <summary>
		/// The mode to use in the rate calculation. By default, "sum" mode is used.
		/// </summary>
		/// <remarks>
		/// The mode may be either "sum", where the rate calculates the sum of field values, or
		/// "value_count", where the rate uses the number of values in the field.
		/// </remarks>
		[DataMember(Name = "mode")]
		RateMode? Mode { get; set; }
	}

	public class RateAggregation : MetricAggregationBase, IRateAggregation
	{
		public RateAggregation(string name) : base(name, null) { }
		public RateAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Rate = this;

		/// <inheritdoc cref ="IRateAggregation.Unit"/>
		public DateInterval? Unit { get; set; }

		/// <inheritdoc cref ="IRateAggregation.Mode"/>
		public RateMode? Mode { get; set; }
	}

	public class RateAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<RateAggregationDescriptor<T>, IRateAggregation, T>, IRateAggregation
		where T : class
	{
		DateInterval? IRateAggregation.Unit { get; set; }

		RateMode? IRateAggregation.Mode { get; set; }
		
		/// <inheritdoc cref ="IRateAggregation.Unit"/>
		public RateAggregationDescriptor<T> Unit(DateInterval? dateInterval) =>
			Assign(dateInterval, (a, v) => a.Unit = v);

		/// <inheritdoc cref ="IRateAggregation.Mode"/>
		public RateAggregationDescriptor<T> Mode(RateMode? mode) =>
			Assign(mode, (a, v) => a.Mode = v);
	}

	[StringEnum]
	public enum RateMode
	{
		/// <summary>
		/// Rate calculates the sum of field values.
		/// </summary>
		[EnumMember(Value = "sum")]
		Sum,

		/// <summary>
		/// Rate uses the number of values in the field.
		/// </summary>
		[EnumMember(Value = "value_count")]
		ValueCount
	}
}
