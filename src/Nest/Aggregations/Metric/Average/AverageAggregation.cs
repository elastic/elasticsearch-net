// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(AverageAggregation))]
	public interface IAverageAggregation : IFormattableMetricAggregation { }

	public class AverageAggregation : FormattableMetricAggregationBase, IAverageAggregation
	{
		internal AverageAggregation() { }

		public AverageAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Average = this;
	}

	public class AverageAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<AverageAggregationDescriptor<T>, IAverageAggregation, T>
			, IAverageAggregation
		where T : class { }
}
