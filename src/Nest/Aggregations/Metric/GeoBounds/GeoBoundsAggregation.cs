using System.Runtime.Serialization;
using Utf8Json;

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
			Assign(a => a.WrapLongitude = wrapLongitude);
	}
}
