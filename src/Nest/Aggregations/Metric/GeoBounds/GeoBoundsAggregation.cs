// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GeoBoundsAggregation))]
	public interface IGeoBoundsAggregation : IMetricAggregation
	{
		[DataMember(Name ="wrap_longitude")]
		bool? WrapLongitude { get; set; }
	}

	public class GeoBoundsAggregation : MetricAggregationBase, IGeoBoundsAggregation
	{
		internal GeoBoundsAggregation() { }

		public GeoBoundsAggregation(string name, Field field) : base(name, field) { }

		public bool? WrapLongitude { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoBounds = this;
	}

	public class GeoBoundsAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<GeoBoundsAggregationDescriptor<T>, IGeoBoundsAggregation, T>
			, IGeoBoundsAggregation
		where T : class
	{
		bool? IGeoBoundsAggregation.WrapLongitude { get; set; }

		public GeoBoundsAggregationDescriptor<T> WrapLongitude(bool? wrapLongitude = true) =>
			Assign(wrapLongitude, (a, v) => a.WrapLongitude = v);
	}
}
