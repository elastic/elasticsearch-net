// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MinAggregation))]
	public interface IMinAggregation : IFormattableMetricAggregation { }

	public class MinAggregation : FormattableMetricAggregationBase, IMinAggregation
	{
		internal MinAggregation() { }

		public MinAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Min = this;
	}

	public class MinAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<MinAggregationDescriptor<T>, IMinAggregation, T>
			, IMinAggregation
		where T : class { }
}
