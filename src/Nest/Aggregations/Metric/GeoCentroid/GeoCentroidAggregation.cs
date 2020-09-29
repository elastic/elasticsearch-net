// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GeoCentroidAggregation))]
	public interface IGeoCentroidAggregation : IMetricAggregation { }

	public class GeoCentroidAggregation : MetricAggregationBase, IGeoCentroidAggregation
	{
		internal GeoCentroidAggregation() { }

		public GeoCentroidAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoCentroid = this;
	}

	public class GeoCentroidAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<GeoCentroidAggregationDescriptor<T>, IGeoCentroidAggregation, T>
			, IGeoCentroidAggregation
		where T : class { }
}
